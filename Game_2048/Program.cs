using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Game_2048
{
    class Program
    {
        // Định nghĩa thực thể cho bảng xếp hạng
        class KyLucEntry
        {
            public string ten { get; set; } = "";
            public int diem { get; set; }
            public string thoiGian { get; set; } = "";
        }

        // Cấu hình động cho game
        static int kichThuoc = 4; // Mặc định là 4x4
        static int tyLeSinhSo2 = 90; // Tỷ lệ sinh số 2 (mặc định 90%)
        
        static int[,] banDo = new int[4, 4];
        static int diemSo = 0;
        static int diemCao = 0;
        static Random rand = new Random();

        // Trả về chuỗi ký tự độ khó để làm tên file lưu trữ
        static string LayTenDoKho()
        {
            switch (tyLeSinhSo2)
            {
                case 100: return "De";
                case 70: return "Kho";
                case 10: return "CucKho";
                default: return "TrungBinh";
            }
        }

        // Đường dẫn file điểm kỷ lục và bảng xếp hạng theo kích thước ma trận và độ khó
        static string fileDiemCao => $"diem_cao_{kichThuoc}x{kichThuoc}_{LayTenDoKho()}.txt";
        static string fileBXH => $"bxh_{kichThuoc}x{kichThuoc}_{LayTenDoKho()}.txt";

        static void Main(string[] args)
        {
            // Thiết lập hiển thị tiếng Việt và Unicode
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=================================================");
                Console.WriteLine("                GAME 2048 ARCADE                 ");
                Console.WriteLine("=================================================");
                Console.ResetColor();
                Console.WriteLine("  1. Chơi Game (Play)");
                Console.WriteLine("  2. Xem Bảng Xếp Hạng (Rank)");
                Console.WriteLine("  3. Thoát Game (Exit)");
                Console.WriteLine("=================================================");
                Console.Write(" Nhập lựa chọn của bạn (1-3): ");

                string luaChonMenu = Console.ReadLine() ?? "";
                switch (luaChonMenu.Trim())
                {
                    case "1":
                        // Bước 1: Cho người chơi thiết lập thông số game
                        HienThiMenuCaiDat();
                        // Bước 2: Vào lượt chơi game
                        VaoLuotChoiGame();
                        break;
                    case "2":
                        // Xem bảng xếp hạng tổng hợp của các kích thước và độ khó
                        HienThiMenuXepHangTongHop();
                        break;
                    case "3":
                        Console.WriteLine("\nCảm ơn bạn đã chơi game! Hẹn gặp lại.");
                        return; // Thoát hẳn chương trình
                    default:
                        Console.WriteLine("\nLựa chọn không hợp lệ! Nhấn phím bất kỳ để tiếp tục...");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        /// <summary>
        /// Logic luồng chơi game chính
        /// </summary>
        static void VaoLuotChoiGame()
        {
            while (true)
            {
                KhoiTaoGame();
                bool dangChoi = true;

                while (dangChoi)
                {
                    VeBanDo();

                    if (KiemTraGameOver())
                    {
                        VeBanDo(); // Vẽ lại trạng thái cuối cùng
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n GAME OVER! Không thể di chuyển được nữa.");
                        Console.ResetColor();

                        // Cập nhật và lưu bảng xếp hạng Top 10
                        CapNhatBangXepHang(diemSo);

                        // Hiển thị bảng xếp hạng của chế độ hiện tại
                        HienThiBangXepHang();
                        
                        Console.Write("\n Bạn có muốn chơi lại không? (Y/N): ");
                        
                        while (true)
                        {
                            ConsoleKeyInfo traLoi = Console.ReadKey(true);
                            if (traLoi.Key == ConsoleKey.Y)
                            {
                                dangChoi = false; // Thoát vòng chơi hiện tại, lặp ngoài sẽ khởi tạo lại game mới
                                break;
                            }
                            else if (traLoi.Key == ConsoleKey.N)
                            {
                                return; // Quay về Menu chính!
                            }
                        }
                        continue;
                    }

                    // Đọc phím bấm từ người chơi
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey phim = keyInfo.Key;

                    if (phim == ConsoleKey.Escape)
                    {
                        // Người chơi bấm ESC để thoát về Menu chính
                        return;
                    }

                    // Lưu điểm số trước khi di chuyển để so sánh phát âm thanh
                    int diemTruoc = diemSo;

                    // Thực hiện di chuyển
                    bool daDiChuyen = DiChuyen(phim);

                    if (daDiChuyen)
                    {
                        // Phát âm thanh phản hồi (Beep)
                        try
                        {
                            if (OperatingSystem.IsWindows())
                            {
                                if (diemSo > diemTruoc)
                                {
                                    // Có gộp ô số -> Tiếng beep cao
                                    Console.Beep(800, 100);
                                }
                                else
                                {
                                    // Chỉ di chuyển -> Tiếng beep thấp, ngắn
                                    Console.Beep(450, 60);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // Bỏ qua lỗi beep
                        }

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
        /// Menu xem bảng xếp hạng tổng hợp theo kích thước và độ khó
        /// </summary>
        static void HienThiMenuXepHangTongHop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================================");
            Console.WriteLine("          XEM BẢNG XẾP HẠNG TOP 10               ");
            Console.WriteLine("=================================================");
            Console.ResetColor();

            // Chọn kích thước muốn xem
            Console.WriteLine("Chọn kích thước ma trận muốn xem:");
            Console.WriteLine("  1. 3 x 3");
            Console.WriteLine("  2. 4 x 4");
            Console.WriteLine("  3. 5 x 5");
            Console.WriteLine("  4. 6 x 6");
            Console.Write("Lựa chọn (1-4, mặc định 2): ");
            string cKichThuoc = Console.ReadLine() ?? "";
            int xemKichThuoc = cKichThuoc.Trim() switch
            {
                "1" => 3,
                "3" => 5,
                "4" => 6,
                _ => 4
            };

            // Chọn độ khó muốn xem
            Console.WriteLine("\nChọn độ khó muốn xem:");
            Console.WriteLine("  1. Dễ");
            Console.WriteLine("  2. Trung bình");
            Console.WriteLine("  3. Khó");
            Console.WriteLine("  4. Siêu khó");
            Console.Write("Lựa chọn (1-4, mặc định 2): ");
            string cDoKho = Console.ReadLine() ?? "";
            int xemTyLeSinhSo2 = cDoKho.Trim() switch
            {
                "1" => 100,
                "3" => 70,
                "4" => 10,
                _ => 90
            };

            // Tạm thời lưu trữ cấu hình hiện tại để khôi phục sau
            int cuKichThuoc = kichThuoc;
            int cuTyLeSinhSo2 = tyLeSinhSo2;

            // Áp dụng cấu hình xem tạm thời
            kichThuoc = xemKichThuoc;
            tyLeSinhSo2 = xemTyLeSinhSo2;

            // Hiển thị bảng xếp hạng
            Console.Clear();
            string sDoKho = tyLeSinhSo2 switch
            {
                100 => "Dễ",
                70 => "Khó",
                10 => "Siêu Khó",
                _ => "Trung Bình"
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================================");
            Console.WriteLine($" BẢNG XẾP HẠNG {kichThuoc}x{kichThuoc} - CHẾ ĐỘ {sDoKho.ToUpper()}");
            Console.WriteLine("=================================================");
            Console.ResetColor();

            HienThiBangXepHang();

            Console.WriteLine("\nNhấn Enter để quay lại menu chính...");
            Console.ReadLine();

            // Khôi phục lại cấu hình ban đầu
            kichThuoc = cuKichThuoc;
            tyLeSinhSo2 = cuTyLeSinhSo2;
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

        #region HỆ THỐNG ĐIỂM KỶ LỤC & BẢNG XẾP HẠNG TOP 10

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

        /// <summary>
        /// Đọc toàn bộ danh sách Top 10 từ file bxh.txt
        /// </summary>
        static List<KyLucEntry> DocBangXepHang()
        {
            List<KyLucEntry> danhSach = new List<KyLucEntry>();
            try
            {
                if (File.Exists(fileBXH))
                {
                    string[] dong = File.ReadAllLines(fileBXH);
                    foreach (string line in dong)
                    {
                        string[] phan = line.Split('|');
                        if (phan.Length == 3 && int.TryParse(phan[1], out int score))
                        {
                            danhSach.Add(new KyLucEntry
                            {
                                ten = phan[0],
                                diem = score,
                                thoiGian = phan[2]
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi
            }

            // Đảm bảo danh sách luôn được sắp xếp giảm dần theo điểm số
            danhSach.Sort((x, y) => y.diem.CompareTo(x.diem));
            return danhSach;
        }

        /// <summary>
        /// Lưu danh sách Top 10 xuống file bxh.txt
        /// </summary>
        static void LuuBangXepHang(List<KyLucEntry> danhSach)
        {
            try
            {
                List<string> dongLuu = new List<string>();
                foreach (var item in danhSach)
                {
                    dongLuu.Add($"{item.ten}|{item.diem}|{item.thoiGian}");
                }
                File.WriteAllLines(fileBXH, dongLuu);
            }
            catch (Exception)
            {
                // Bỏ qua lỗi
            }
        }

        /// <summary>
        /// Cập nhật điểm của lượt chơi vừa kết thúc vào BXH Top 10
        /// </summary>
        static void CapNhatBangXepHang(int diemMoi)
        {
            var bxh = DocBangXepHang();

            // Nếu chưa đủ 10 người hoặc điểm mới cao hơn điểm của vị trí thứ 10 hiện tại
            if (bxh.Count < 10 || diemMoi > bxh[bxh.Count - 1].diem)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n 🎉 KỶ LỤC MỚI! Điểm số {diemMoi} lọt vào TOP 10 Bảng Xếp Hạng!");
                Console.Write(" Vui lòng nhập tên của bạn (viết liền hoặc không dấu): ");
                Console.ResetColor();

                string tenNguoiChoi = Console.ReadLine() ?? "VoDanh";
                if (string.IsNullOrWhiteSpace(tenNguoiChoi)) tenNguoiChoi = "VoDanh";

                // Thêm kết quả mới
                bxh.Add(new KyLucEntry
                {
                    ten = tenNguoiChoi,
                    diem = diemMoi,
                    thoiGian = DateTime.Now.ToString("dd/MM/yyyy")
                });

                // Sắp xếp lại danh sách
                bxh.Sort((x, y) => y.diem.CompareTo(x.diem));

                // Giữ lại tối đa 10 người
                if (bxh.Count > 10)
                {
                    bxh.RemoveAt(10);
                }

                // Lưu lại
                LuuBangXepHang(bxh);
            }
        }

        /// <summary>
        /// Vẽ bảng xếp hạng Top 10 lên Console
        /// </summary>
        static void HienThiBangXepHang()
        {
            var bxh = DocBangXepHang();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"{"Hạng",-6}{"Tên người chơi",-18}{"Điểm số",-10}{"Ngày chơi"}");
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();

            if (bxh.Count == 0)
            {
                Console.WriteLine(" Chưa có kỷ lục nào được ghi nhận ở chế độ này.");
            }
            else
            {
                for (int i = 0; i < bxh.Count; i++)
                {
                    // Đổi màu sắc cho 3 thứ hạng đầu
                    if (i == 0) Console.ForegroundColor = ConsoleColor.Yellow; // Vàng
                    else if (i == 1) Console.ForegroundColor = ConsoleColor.Gray; // Bạc
                    else if (i == 2) Console.ForegroundColor = ConsoleColor.DarkYellow; // Đồng

                    Console.WriteLine($"{i + 1,-6}{bxh[i].ten,-18}{bxh[i].diem,-10}{bxh[i].thoiGian}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("-------------------------------------------------");
        }

        #endregion
    }
}
