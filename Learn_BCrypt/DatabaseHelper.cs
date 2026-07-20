using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System;

namespace Learn_BCrypt
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=localhost;Database=SecurityDemoDb;User=root;Password=root;Port=3306;";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public void InitializeDatabase()
        {
            var builder = new MySqlConnectionStringBuilder(_connectionString);
            string targetDatabase = builder.Database;

            // Connect to server without database first to create it if it doesn't exist
            builder.Database = "";
            string serverConnectionString = builder.ConnectionString;

            using (var serverConn = new MySqlConnection(serverConnectionString))
            {
                serverConn.Open();
                using (var cmd = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS `{targetDatabase}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;", serverConn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Now connect to the database and create tables
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                // 1. Create VaiTro table
                string createVaiTroTable = @"
                    CREATE TABLE IF NOT EXISTS VaiTro (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        TenVaiTro VARCHAR(50) NOT NULL UNIQUE,
                        MoTa VARCHAR(255) NULL
                    );";
                using (var cmd = new MySqlCommand(createVaiTroTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2. Create TaiKhoan table
                string createTaiKhoanTable = @"
                    CREATE TABLE IF NOT EXISTS TaiKhoan (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
                        MatKhauHash VARCHAR(255) NOT NULL,
                        NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";
                using (var cmd = new MySqlCommand(createTaiKhoanTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // 3. Create NguoiDung table
                string createNguoiDungTable = @"
                    CREATE TABLE IF NOT EXISTS NguoiDung (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        HoTen VARCHAR(100) NOT NULL,
                        Tuoi INT NULL,
                        QueQuan VARCHAR(150) NULL,
                        NoiSinh VARCHAR(150) NULL,
                        TaiKhoanId INT NOT NULL,
                        VaiTroId INT NULL,
                        FOREIGN KEY (TaiKhoanId) REFERENCES TaiKhoan(Id) ON DELETE CASCADE,
                        FOREIGN KEY (VaiTroId) REFERENCES VaiTro(Id) ON DELETE SET NULL
                    );";
                using (var cmd = new MySqlCommand(createNguoiDungTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // 4. Insert default roles if table is empty
                string checkRolesExist = "SELECT COUNT(*) FROM VaiTro;";
                long roleCount = 0;
                using (var cmd = new MySqlCommand(checkRolesExist, conn))
                {
                    var result = cmd.ExecuteScalar();
                    roleCount = Convert.ToInt64(result);
                }

                if (roleCount == 0)
                {
                    string insertRoles = @"
                        INSERT INTO VaiTro (TenVaiTro, MoTa) VALUES 
                        ('Developer', 'Lập trình viên phát triển sản phẩm'),
                        ('Tester', 'Kiểm thử viên kiểm soát chất lượng'),
                        ('QA', 'Đảm bảo quy trình chất lượng phần mềm'),
                        ('Manager', 'Quản lý dự án');";
                    using (var cmd = new MySqlCommand(insertRoles, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
