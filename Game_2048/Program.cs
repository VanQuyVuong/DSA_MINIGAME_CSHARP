using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Game_2048
{
    class Program
    {
        // Cấu hình động cho game
        static int kichThuoc = 4; // Mặc định là 4x4
        static int tyLeSinhSo2 = 90; // Tỷ lệ sinh số 2 (mặc định 90%)
        
        static int[,] banDo = new int[4, 4];
        static int diemSo = 0;
        static int diemCao = 0;
        static Random rand = new Random();

        // Đường dẫn file điểm cao thay đổi động theo kích thước ma trận
        static string fileDiemCao => $"diem_cao_{kichThuoc}x{kichThuoc}.txt";

        static void Main(string[] args)
        {
            // Thiết lập hiển thị tiếng Việt và Unicode
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            while (true)
            {
                // Bước 1: Cho người chơi thiết lập thông số game
                HienThiMenuCaiDat();

                // Bước 2: Khởi tạo game mới dựa trên cài đặt
                KhoiTaoGame();
                bool dangChoi = true;

                while (dangChoi)
                {
                    VeBanDo();

                    if (KiemTraGameOver())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n GAME OVER! Không thể di chuyển được nữa.");
                        Console.ResetColor();
                        Console.Write(" Bạn có muốn chơi lại không? (Y/N): ");
                        
                        while (true)
                        {
                            ConsoleKeyInfo traLoi = Console.ReadKey(true);
                            if (traLoi.Key == ConsoleKey.Y)
                            {
                                dangChoi = false; // Thoát vòng lặp hiện tại để bắt đầu game mới
                                break;
                            }
                            else if (traLoi.Key == ConsoleKey.N)
                            {
                                Console.WriteLine("\nCảm ơn bạn đã chơi game! Hẹn gặp lại.");
                                return; // Thoát toàn bộ chương trình
                            }
                        }
                        continue;
                    }

                    // Đọc phím bấm từ người chơi
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey phim = keyInfo.Key;

                    if (phim == ConsoleKey.Escape)
                    {
                        Console.WriteLine("\nĐang thoát game...");
                        return;
                    }

                    // Thực hiện di chuyển
                    bool daDiChuyen = DiChuyen(phim);

                    if (daDiChuyen)
                    {
                        // Sinh thêm một số mới
                        SinhSoNgauNhien();
                    }
                }
            }
        }

        /// <summary>
        /// Menu cài đặt trước khi vào game để chọn kích thước và độ khó
        /// </summary>
        static void HienThiMenuCaiDat()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================================");
            Console.WriteLine("          THIẾT LẬP GAME 2048 CUSTOM             ");
            Console.WriteLine("=================================================");
            Console.ResetColor();

            // 1. Chọn kích thước ma trận
            Console.WriteLine("\n[1] CHỌN KÍCH THƯỚC MA TRẬN:");
            Console.WriteLine("  a. 3 x 3 (Không gian chật hẹp - Rất khó)");
            Console.WriteLine("  b. 4 x 4 (Bản chuẩn gốc - Cân bằng)");
            Console.WriteLine("  c. 5 x 5 (Rộng rãi - Dễ chơi)");
            Console.WriteLine("  d. 6 x 6 (Khổng lồ - Siêu dễ)");
            Console.Write("Nhập lựa chọn của bạn (a/b/c/d, mặc định b): ");
            string luaChonKichThuoc = Console.ReadLine() ?? "";
            
            switch (luaChonKichThuoc.ToLower().Trim())
            {
                case "a":
                    kichThuoc = 3;
                    break;
                case "c":
                    kichThuoc = 5;
                    break;
                case "d":
                    kichThuoc = 6;
                    break;
                default:
                    kichThuoc = 4;
                    break;
            }

            // 2. Chọn độ khó (Xác suất sinh số 2 và 4)
            Console.WriteLine("\n[2] CHỌN ĐỘ KHÓ (Tỷ lệ sinh số):");
            Console.WriteLine("  1. Dễ (Chỉ sinh số 2 - Dễ gộp số)");
            Console.WriteLine("  2. Trung bình (Bản gốc - 90% số 2, 10% số 4)");
            Console.WriteLine("  3. Khó (70% số 2, 30% số 4)");
            Console.WriteLine("  4. Siêu khó (90% số 4, 10% số 2)");
            Console.Write("Nhập lựa chọn của bạn (1/2/3/4, mặc định 2): ");
            string luaChonDoKho = Console.ReadLine() ?? "";

            switch (luaChonDoKho.Trim())
            {
                case "1":
                    tyLeSinhSo2 = 100;
                    break;
                case "3":
                    tyLeSinhSo2 = 70;
                    break;
                case "4":
                    tyLeSinhSo2 = 10;
                    break;
                default:
                    tyLeSinhSo2 = 90;
                    break;
            }

            Console.WriteLine("\n=> Thiết lập hoàn tất! Nhấn Enter để bắt đầu chơi...");
            Console.ReadLine();
        }

        /// <summary>
        /// Khởi tạo game: Khởi tạo mảng động theo kích thước và sinh 2 số ngẫu nhiên ban đầu
        /// </summary>
        static void KhoiTaoGame()
        {
            banDo = new int[kichThuoc, kichThuoc];
            diemSo = 0;
            diemCao = DocDiemCao();

            // Sinh 2 số ngẫu nhiên ban đầu
            SinhSoNgauNhien();
            SinhSoNgauNhien();
        }

        /// <summary>
        /// Sinh ngẫu nhiên số 2 hoặc 4 vào một ô trống dựa trên độ khó đã thiết lập
        /// </summary>
        static void SinhSoNgauNhien()
        {
            List<(int dong, int cot)> oTrong = new List<(int, int)>();
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    if (banDo[i, j] == 0)
                    {
                        oTrong.Add((i, j));
                    }
                }
            }

            if (oTrong.Count > 0)
            {
                int viTriNgauNhien = rand.Next(oTrong.Count);
                var oDuocChon = oTrong[viTriNgauNhien];

                // Sinh số theo độ khó
                banDo[oDuocChon.dong, oDuocChon.cot] = rand.Next(100) < tyLeSinhSo2 ? 2 : 4;
            }
        }

        /// <summary>
        /// Dồn tất cả các ô số sang bên TRÁI và thực hiện gộp nếu có các ô cạnh nhau bằng nhau.
        /// </summary>
        static bool DonTrai()
        {
            bool daThayDoi = false;

            for (int i = 0; i < kichThuoc; i++)
            {
                // Bước 1: Dồn các số khác 0 về bên trái của dòng i
                int[] dongMoi = new int[kichThuoc];
                int index = 0;
                for (int j = 0; j < kichThuoc; j++)
                {
                    if (banDo[i, j] != 0)
                    {
                        dongMoi[index++] = banDo[i, j];
                    }
                }

                // Bước 2: Gộp các số giống nhau nằm cạnh nhau
                for (int j = 0; j < kichThuoc - 1; j++)
                {
                    if (dongMoi[j] != 0 && dongMoi[j] == dongMoi[j + 1])
                    {
                        dongMoi[j] *= 2;
                        diemSo += dongMoi[j];
                        dongMoi[j + 1] = 0;
                        j++; // Bỏ qua ô tiếp theo đã bị gộp
                    }
                }

                // Bước 3: Dồn lại các số khác 0 về bên trái sau khi gộp
                int[] dongCuoi = new int[kichThuoc];
                index = 0;
                for (int j = 0; j < kichThuoc; j++)
                {
                    if (dongMoi[j] != 0)
                    {
                        dongCuoi[index++] = dongMoi[j];
                    }
                }

                // Bước 4: Kiểm tra sự thay đổi của dòng, cập nhật kết quả đè lại banDo
                for (int j = 0; j < kichThuoc; j++)
                {
                    if (banDo[i, j] != dongCuoi[j])
                    {
                        daThayDoi = true;
                    }
                    banDo[i, j] = dongCuoi[j];
                }
            }

            return daThayDoi;
        }

        /// <summary>
        /// Xoay ma trận banDo 90 độ theo chiều kim đồng hồ (Hỗ trợ kích thước động)
        /// </summary>
        static void XoayMaTran90()
        {
            int[,] temp = new int[kichThuoc, kichThuoc];

            // Áp dụng công thức xoay ma trận xoay quanh kíchThuoc
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    temp[j, (kichThuoc - 1) - i] = banDo[i, j];
                }
            }

            // Sao chép temp đè ngược lại banDo
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    banDo[i, j] = temp[i, j];
                }
            }
        }

        /// <summary>
        /// Quản lý di chuyển theo 4 hướng sử dụng xoay ma trận xoay quanh DonTrai()
        /// </summary>
        static bool DiChuyen(ConsoleKey phim)
        {
            bool daDiChuyen = false;

            switch (phim)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    daDiChuyen = DonTrai();
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    XoayMaTran90();
                    XoayMaTran90();
                    daDiChuyen = DonTrai();
                    XoayMaTran90();
                    XoayMaTran90();
                    break;

                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    XoayMaTran90();
                    XoayMaTran90();
                    XoayMaTran90();
                    daDiChuyen = DonTrai();
                    XoayMaTran90();
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    XoayMaTran90();
                    daDiChuyen = DonTrai();
                    XoayMaTran90();
                    XoayMaTran90();
                    XoayMaTran90();
                    break;
            }

            return daDiChuyen;
        }

        /// <summary>
        /// Kiểm tra điều kiện Game Over cho kích thước động
        /// </summary>
        static bool KiemTraGameOver()
        {
            // 1. Kiểm tra xem còn ô trống không
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    if (banDo[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            // 2. Kiểm tra xem các ô cạnh nhau có bằng nhau không
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    // Kiểm tra ô bên phải
                    if (j < kichThuoc - 1 && banDo[i, j] == banDo[i, j + 1])
                    {
                        return false;
                    }
                    // Kiểm tra ô bên dưới
                    if (i < kichThuoc - 1 && banDo[i, j] == banDo[i + 1, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Vẽ giao diện game co giãn động theo kích thước ma trận
        /// </summary>
        static void VeBanDo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            int chieuRongKhung = kichThuoc * 7 + 1;
            string duongVienNgang = new string('=', chieuRongKhung);
            
            Console.WriteLine(duongVienNgang);
            Console.WriteLine($"      GAME 2048 CUSTOM ({kichThuoc}x{kichThuoc})      ");
            Console.WriteLine(duongVienNgang);
            Console.ResetColor();

            if (diemSo > diemCao)
            {
                diemCao = diemSo;
                LuuDiemCao(diemCao);
            }
            Console.WriteLine($" Điểm số: {diemSo}  |  Kỷ lục: {diemCao}");
            Console.WriteLine(new string('-', chieuRongKhung));

            for (int i = 0; i < kichThuoc; i++)
            {
                // In khoảng trống dòng trên của các ô
                for (int j = 0; j < kichThuoc; j++)
                {
                    Console.Write("|      ");
                }
                Console.WriteLine("|");

                // In giá trị các ô số ở giữa
                for (int j = 0; j < kichThuoc; j++)
                {
                    Console.Write("|");
                    int so = banDo[i, j];
                    if (so == 0)
                    {
                        Console.Write("      ");
                    }
                    else
                    {
                        ThietLapMauSac(so);
                        // Căn lề số cho thẳng hàng
                        Console.Write($"{so,4}  ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine("|");

                // In đường gạch chân phân cách các ô
                for (int j = 0; j < kichThuoc; j++)
                {
                    Console.Write("|______");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n[W, A, S, D] hoặc [Phím Mũi Tên] để chơi.");
            Console.WriteLine("[ESC] để quay lại menu chính.");
        }

        /// <summary>
        /// Thiết lập màu sắc hiển thị
        /// </summary>
        static void ThietLapMauSac(int so)
        {
            switch (so)
            {
                case 2:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 16:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case 32:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 64:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 128:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case 256:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case 512:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 1024:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case 2048:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
            }
        }

        /// <summary>
        /// Đọc điểm kỷ lục từ file lưu trữ
        /// </summary>
        static int DocDiemCao()
        {
            try
            {
                if (File.Exists(fileDiemCao))
                {
                    string noiDung = File.ReadAllText(fileDiemCao);
                    if (int.TryParse(noiDung, out int diem))
                    {
                        return diem;
                    }
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi đọc file
            }
            return 0;
        }

        /// <summary>
        /// Lưu điểm kỷ lục mới vào file
        /// </summary>
        static void LuuDiemCao(int diem)
        {
            try
            {
                File.WriteAllText(fileDiemCao, diem.ToString());
            }
            catch (Exception)
            {
                // Bỏ qua lỗi ghi file
            }
        }
    }
}
