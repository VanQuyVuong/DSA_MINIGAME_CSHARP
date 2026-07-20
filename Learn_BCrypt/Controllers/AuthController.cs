using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Learn_BCrypt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Learn_BCrypt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseHelper _dbHelper;

        public AuthController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        /// <summary>
        /// Lấy danh sách các vai trò (Roles) có trong hệ thống để chọn khi đăng ký.
        /// </summary>
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = new List<RoleModel>();
            try
            {
                using var conn = _dbHelper.GetConnection();
                await conn.OpenAsync();
                
                string query = "SELECT Id, TenVaiTro, MoTa FROM VaiTro;";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    roles.Add(new RoleModel
                    {
                        Id = reader.GetInt32("Id"),
                        TenVaiTro = reader.GetString("TenVaiTro"),
                        MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString("MoTa")
                    });
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        /// <summary>
        /// Đăng ký tài khoản người dùng mới.
        /// Mật khẩu sẽ được băm bằng thuật toán BCrypt trước khi lưu vào database.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TenDangNhap) || string.IsNullOrWhiteSpace(request.MatKhau))
            {
                return BadRequest("Tên đăng nhập và mật khẩu không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(request.HoTen))
            {
                return BadRequest("Họ tên không được để trống.");
            }

            try
            {
                using var conn = _dbHelper.GetConnection();
                await conn.OpenAsync();

                // 1. Kiểm tra xem tên đăng nhập đã tồn tại chưa
                string checkUserQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @username;";
                using (var checkCmd = new MySqlCommand(checkUserQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@username", request.TenDangNhap.Trim());
                    long count = Convert.ToInt64(await checkCmd.ExecuteScalarAsync());
                    if (count > 0)
                    {
                        return BadRequest("Tên đăng nhập đã tồn tại trong hệ thống.");
                    }
                }

                // 2. Băm mật khẩu bằng BCrypt
                // BCrypt tự động tạo Salt ngẫu nhiên và nhúng trực tiếp vào chuỗi Hash kết quả.
                // Work Factor mặc định là 11 (mức cân bằng tốt giữa bảo mật và tốc độ).
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.MatKhau);

                // 3. Thực hiện lưu vào Database bằng Transaction để đảm bảo tính toàn vẹn dữ liệu
                using var transaction = await conn.BeginTransactionAsync();
                try
                {
                    // Chèn tài khoản mới
                    string insertAccountQuery = @"
                        INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash) 
                        VALUES (@username, @passwordHash);";
                    
                    int accountId = 0;
                    using (var cmd = new MySqlCommand(insertAccountQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@username", request.TenDangNhap.Trim());
                        cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                        await cmd.ExecuteNonQueryAsync();
                        
                        // Lấy Id của tài khoản vừa chèn
                        accountId = (int)cmd.LastInsertedId;
                    }

                    // Chèn thông tin cá nhân người dùng liên kết với tài khoản vừa tạo
                    string insertUserQuery = @"
                        INSERT INTO NguoiDung (HoTen, Tuoi, QueQuan, NoiSinh, TaiKhoanId, VaiTroId)
                        VALUES (@hoTen, @tuoi, @queQuan, @noiSinh, @taiKhoanId, @vaiTroId);";
                    
                    using (var cmd = new MySqlCommand(insertUserQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@hoTen", request.HoTen.Trim());
                        cmd.Parameters.AddWithValue("@tuoi", (object?)request.Tuoi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@queQuan", (object?)request.QueQuan?.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@noiSinh", (object?)request.NoiSinh?.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@taiKhoanId", accountId);
                        cmd.Parameters.AddWithValue("@vaiTroId", (object?)request.VaiTroId ?? DBNull.Value);
                        
                        await cmd.ExecuteNonQueryAsync();
                    }

                    await transaction.CommitAsync();
                    return Ok(new { 
                        Message = "Đăng ký tài khoản thành công!", 
                        Username = request.TenDangNhap,
                        MatKhauDaBam = hashedPassword
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Lỗi trong quá trình tạo tài khoản: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        /// <summary>
        /// Đăng nhập hệ thống.
        /// Sử dụng BCrypt.Verify để kiểm tra mật khẩu người dùng nhập vào với Hash lấy ra từ DB.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TenDangNhap) || string.IsNullOrWhiteSpace(request.MatKhau))
            {
                return BadRequest("Vui lòng cung cấp đầy đủ tên đăng nhập và mật khẩu.");
            }

            try
            {
                using var conn = _dbHelper.GetConnection();
                await conn.OpenAsync();

                // 1. Lấy thông tin tài khoản và mật khẩu đã băm từ DB
                int accountId = 0;
                string hashedPasswordFromDb = string.Empty;

                string queryAccount = "SELECT Id, MatKhauHash FROM TaiKhoan WHERE TenDangNhap = @username;";
                using (var cmd = new MySqlCommand(queryAccount, conn))
                {
                    cmd.Parameters.AddWithValue("@username", request.TenDangNhap.Trim());
                    using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        accountId = reader.GetInt32("Id");
                        hashedPasswordFromDb = reader.GetString("MatKhauHash");
                    }
                    else
                    {
                        // Không tìm thấy tên đăng nhập
                        return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
                    }
                }

                // 2. Xác thực mật khẩu bằng thư viện BCrypt.Verify
                // BCrypt sẽ tự động tách Salt từ chuỗi hash trong DB, đem băm mật khẩu người dùng nhập vào với Salt đó, và so sánh.
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.MatKhau, hashedPasswordFromDb);

                if (!isPasswordValid)
                {
                    return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
                }

                // 3. Nếu đúng mật khẩu, lấy toàn bộ thông tin cá nhân và vai trò liên kết
                string queryProfile = @"
                    SELECT nd.Id as NguoiDungId, nd.HoTen, nd.Tuoi, nd.QueQuan, nd.NoiSinh, tk.TenDangNhap, vt.TenVaiTro, vt.MoTa as MoTaVaiTro
                    FROM NguoiDung nd
                    INNER JOIN TaiKhoan tk ON nd.TaiKhoanId = tk.Id
                    LEFT JOIN VaiTro vt ON nd.VaiTroId = vt.Id
                    WHERE tk.Id = @accountId;";

                UserProfile? profile = null;
                using (var cmd = new MySqlCommand(queryProfile, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        profile = new UserProfile
                        {
                            NguoiDungId = reader.GetInt32("NguoiDungId"),
                            HoTen = reader.GetString("HoTen"),
                            Tuoi = reader.IsDBNull(reader.GetOrdinal("Tuoi")) ? null : reader.GetInt32("Tuoi"),
                            QueQuan = reader.IsDBNull(reader.GetOrdinal("QueQuan")) ? null : reader.GetString("QueQuan"),
                            NoiSinh = reader.IsDBNull(reader.GetOrdinal("NoiSinh")) ? null : reader.GetString("NoiSinh"),
                            TenDangNhap = reader.GetString("TenDangNhap"),
                            TenVaiTro = reader.IsDBNull(reader.GetOrdinal("TenVaiTro")) ? null : reader.GetString("TenVaiTro"),
                            MoTaVaiTro = reader.IsDBNull(reader.GetOrdinal("MoTaVaiTro")) ? null : reader.GetString("MoTaVaiTro")
                        };
                    }
                }

                if (profile == null)
                {
                    return StatusCode(500, "Xác thực thành công nhưng không tìm thấy thông tin chi tiết người dùng.");
                }

                return Ok(new { Message = "Đăng nhập thành công!", User = profile });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        /// <summary>
        /// Demo trực quan về cơ chế băm của BCrypt.
        /// Chức năng này băm cùng một mật khẩu 3 lần để bạn thấy 3 kết quả hash hoàn toàn khác nhau.
        /// </summary>
        [HttpGet("demo-bcrypt")]
        public IActionResult DemoBCrypt([FromQuery] string password = "mật_khẩu_123")
        {
            var result = new BCryptDemoResult
            {
                MatKhauGoc = password,
                Hashes = new List<string>(),
                GiaiThich = "Mặc dù cùng 1 mật khẩu, nhưng mỗi lần băm bằng BCrypt.HashPassword() sẽ sinh ra một chuỗi hash hoàn toàn khác nhau. " +
                            "Lý do là vì BCrypt tự động tạo ra một 'Salt' (muối) ngẫu nhiên mỗi lần chạy và tích hợp nó trực tiếp vào chuỗi hash kết quả. " +
                            "Khi xác thực bằng BCrypt.Verify(password, hash), BCrypt sẽ tự động trích xuất muối từ chuỗi hash đó để băm lại mật khẩu người dùng nhập vào và so sánh. " +
                            "Điều này ngăn ngừa tấn công Rainbow Table (bảng băm tính toán sẵn)."
            };

            for (int i = 0; i < 3; i++)
            {
                // Băm mật khẩu
                string hash = BCrypt.Net.BCrypt.HashPassword(password);
                result.Hashes.Add(hash);
            }

            return Ok(result);
        }
    }
}
