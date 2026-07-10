using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game_Snake
{
    class Program
    {
        // Kích thước bản đồ chơi
        static int chieuCao = 15;
        static int chieuRong = 30;

        // Cấu trúc dữ liệu LinkedList đại diện cho cơ thể rắn
        static LinkedList<(int dong, int cot)> thanRan = new LinkedList<(int, int)>();

        // Hướng di chuyển hiện tại của rắn
        static string huongDi = "RIGHT";

        // Tọa độ của mồi và đối tượng sinh số ngẫu nhiên
        static (int dong, int cot) moi;
        static Random rand = new Random();

        // Danh sách chướng ngại vật ngẫu nhiên (cho Chế độ 3)
        static List<(int dong, int cot)> chuongNgaiVat = new List<(int, int)>();

        // Các mẫu chướng ngại vật đa dạng (đơn, đôi, ba chữ L, 4 ô dọc, chéo, vuông 2x2)
        static List<List<(int d, int c)>> mauChuongNgaiVat = new List<List<(int, int)>>
        {
            new List<(int, int)> { (0, 0) }, // 1. Vật cản đơn
            new List<(int, int)> { (0, 0), (0, 1) }, // 2. Vật cản đôi (ngang)
            new List<(int, int)> { (0, 0), (1, 0) }, // 3. Vật cản đôi (dọc)
            new List<(int, int)> { (0, 0), (1, 0), (1, 1) }, // 4. Vật cản ba chữ L
            new List<(int, int)> { (0, 0), (1, 0), (2, 0), (3, 0) }, // 5. Khối 4 dọc
            new List<(int, int)> { (0, 0), (1, 1), (2, 2) }, // 6. Đường chéo 3 ô
            new List<(int, int)> { (0, 0), (0, 1), (1, 0), (1, 1) } // 7. Cụm vuông 2x2
        };

        // Biến kiểm soát trạng thái trò chơi
        static bool dangChoi = true;
        static int cheDoChoi = 1; // 1: Tự do, 2: Bức tường, 3: Chướng ngại vật
        static int diemSo = 0;
        static int dotTangTruong = 0; // Bộ đệm tăng trưởng đốt thân rắn

        // Đếm ngược thời gian đổi địa hình (Chế độ 3)
        static DateTime thoiDiemDoiChuongNgaiVat;
        static int thoiGianChuKy = 15; // Cứ 15 giây đổi địa hình 1 lần

        // Tên file bảng xếp hạng động theo từng chế độ
        private static string fileBXH => $"bxh_snake_mode{cheDoChoi}.txt";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=================================================");
                Console.WriteLine("          GAME RẮN SĂN MỒI (SNAKE CONSOLE)       ");
                Console.WriteLine("=================================================");
                Console.ResetColor();
                Console.WriteLine("  1. Bắt đầu chơi (Play)");
                Console.WriteLine("  2. Xem Bảng xếp hạng (Leaderboards)");
                Console.WriteLine("  3. Thoát chương trình (Exit)");
                Console.WriteLine("=================================================");
                Console.Write(" Chọn tùy chọn (1-3): ");

                string luaChon = Console.ReadLine() ?? "";
                if (luaChon == "3")
                {
                    Console.WriteLine("\n Cảm ơn bạn đã chơi game! Tạm biệt.");
                    break;
                }
                else if (luaChon == "2")
                {
                    HienThiMenuBXH();
                }
                else if (luaChon == "1")
                {
                    ChọnCheDoChoi();
                    ChayVongLapGame();
                }
            }
        }

        static void ChọnCheDoChoi()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=================================================");
                Console.WriteLine("             CHỌN CHẾ ĐỘ CHƠI (GAME MODE)        ");
                Console.WriteLine("=================================================");
                Console.ResetColor();
                Console.WriteLine(" 1. Chế độ Tự do (Xuyên tường, dài thêm 2 đốt/mồi, Điểm x1)");
                Console.WriteLine(" 2. Chế độ Bức tường (Tường là vật cản, dài 1 đốt, Điểm x2)");
                Console.WriteLine(" 3. Chế độ Chướng ngại vật (Đổi địa hình mỗi 15s, Điểm x3 + Thưởng)");
                Console.WriteLine("=================================================");
                Console.Write(" Chọn chế độ (1-3): ");

                string input = Console.ReadLine() ?? "";
                if (input == "1" || input == "2" || input == "3")
                {
                    cheDoChoi = int.Parse(input);
                    break;
                }
            }
        }

        static void ChayVongLapGame()
        {
            // Reset các biến trạng thái game
            dangChoi = true;
            diemSo = 0;
            dotTangTruong = 0;
            huongDi = "RIGHT";
            thoiDiemDoiChuongNgaiVat = DateTime.Now;

            KhoiTaoRan();

            // Vòng lặp chạy game
            while (dangChoi)
            {
                VeBanDo();
                DocPhimDieuKhien();
                DiChuyen();

                // Kiểm tra đổi địa hình chướng ngại vật mỗi 15 giây (Chế độ 3)
                if (cheDoChoi == 3 && (DateTime.Now - thoiDiemDoiChuongNgaiVat).TotalSeconds >= thoiGianChuKy)
                {
                    diemSo += 5; // Thưởng 5 điểm vì sống sót qua đợt đổi địa hình
                    SinhChuongNgaiVatNgauNhien();
                    thoiDiemDoiChuongNgaiVat = DateTime.Now; // Reset mốc thời gian
                }

                // Tốc độ game (150ms)
                Thread.Sleep(150);
            }

            // Màn hình hiển thị khi Game Over
            Console.Clear();
            VeBanDo(); 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n 💥 GAME OVER! Rắn đã va chạm!");
            Console.ResetColor();
            Console.WriteLine($" Điểm số cuối cùng của bạn: {diemSo}");

            // Cập nhật kỷ lục của người chơi
            CapNhatBangXepHang(diemSo);

            // Hiển thị bảng xếp hạng của chế độ vừa chơi
            HienThiBangXepHang();

            Console.WriteLine("\n Nhấn Enter để quay lại Menu chính...");
            Console.ReadLine();
        }

        static void KhoiTaoRan()
        {
            thanRan.Clear();
            chuongNgaiVat.Clear();

            // Thêm 3 đốt rắn ban đầu
            thanRan.AddLast((7, 10)); // Đầu rắn
            thanRan.AddLast((7, 9));  
            thanRan.AddLast((7, 8));  

            // Sinh mồi lần đầu tiên
            SinhMoi();

            // Sinh chướng ngại vật ngẫu nhiên nếu chọn Chế độ 3
            if (cheDoChoi == 3)
            {
                SinhChuongNgaiVatNgauNhien();
            }
        }

        /// <summary>
        /// Thuật toán sinh chướng ngại vật phức hợp không đè lên rắn và mồi
        /// </summary>
        static void SinhChuongNgaiVatNgauNhien()
        {
            chuongNgaiVat.Clear();
            // Sinh khoảng 4 cụm chướng ngại vật đa dạng hình dáng
            int soCum = 4;
            for (int k = 0; k < soCum; k++)
            {
                var mau = mauChuongNgaiVat[rand.Next(mauChuongNgaiVat.Count)];
                bool thanhCong = false;
                int thuLai = 0;

                while (!thanhCong && thuLai < 100)
                {
                    thuLai++;
                    // Tránh sinh sát mép viền để không bị bít đường đi
                    int baseD = rand.Next(2, chieuCao - 4);
                    int baseC = rand.Next(2, chieuRong - 4);

                    bool viTriHopLe = true;
                    List<(int d, int c)> cumTamThoi = new List<(int, int)>();

                    foreach (var offset in mau)
                    {
                        int targetD = baseD + offset.d;
                        int targetC = baseC + offset.c;

                        // 1. Kiểm tra biên bản đồ an toàn
                        if (targetD < 1 || targetD >= chieuCao - 1 || targetC < 1 || targetC >= chieuRong - 1)
                        {
                            viTriHopLe = false;
                            break;
                        }

                        // 2. Tránh đè lên bất kỳ phần thân nào của rắn hiện tại
                        if (thanRan.Contains((targetD, targetC)))
                        {
                            viTriHopLe = false;
                            break;
                        }

                        // 3. Tránh đè lên mồi
                        if (targetD == moi.dong && targetC == moi.cot)
                        {
                            viTriHopLe = false;
                            break;
                        }

                        // 4. Tránh đè lên chướng ngại vật cũ
                        if (chuongNgaiVat.Contains((targetD, targetC)) || cumTamThoi.Contains((targetD, targetC)))
                        {
                            viTriHopLe = false;
                            break;
                        }

                        cumTamThoi.Add((targetD, targetC));
                    }

                    if (viTriHopLe)
                    {
                        foreach (var o in cumTamThoi)
                        {
                            chuongNgaiVat.Add(o);
                        }
                        thanhCong = true;
                    }
                }
            }
        }

        static void SinhMoi()
        {
            while (true)
            {
                int d = rand.Next(chieuCao);
                int c = rand.Next(chieuRong);

                // Không sinh mồi đè lên thân rắn hoặc đè lên chướng ngại vật
                if (!thanRan.Contains((d, c)) && !chuongNgaiVat.Contains((d, c)))
                {
                    moi = (d, c);
                    break;
                }
            }
        }

        static void DocPhimDieuKhien()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo phim = Console.ReadKey(true);
                switch (phim.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (huongDi != "DOWN") huongDi = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (huongDi != "UP") huongDi = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (huongDi != "RIGHT") huongDi = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if (huongDi != "LEFT") huongDi = "RIGHT";
                        break;
                }
            }
        }

        static void DiChuyen()
        {
            var dauHienTai = thanRan.First!.Value;

            int dongMoi = dauHienTai.dong;
            int cotMoi = dauHienTai.cot;

            switch (huongDi)
            {
                case "UP": dongMoi--; break;
                case "DOWN": dongMoi++; break;
                case "LEFT": cotMoi--; break;
                case "RIGHT": cotMoi++; break;
            }

            // Xử lý theo từng chế độ chơi
            if (cheDoChoi == 1)
            {
                // Chế độ Tự do: Đi xuyên tường
                dongMoi = (dongMoi + chieuCao) % chieuCao;
                cotMoi = (cotMoi + chieuRong) % chieuRong;
            }
            else
            {
                // Chế độ Bức tường & Chướng ngại vật: Húc tường là chết
                if (dongMoi < 0 || dongMoi >= chieuCao || cotMoi < 0 || cotMoi >= chieuRong)
                {
                    dangChoi = false;
                    return;
                }
            }

            // Kiểm tra va chạm với chướng ngại vật trong (Chế độ 3)
            if (cheDoChoi == 3 && chuongNgaiVat.Contains((dongMoi, cotMoi)))
            {
                dangChoi = false;
                return;
            }

            // Kiểm tra tự cắn chính mình (va chạm thân)
            if (thanRan.Contains((dongMoi, cotMoi)))
            {
                dangChoi = false;
                return;
            }

            // Thêm đầu mới
            thanRan.AddFirst((dongMoi, cotMoi));

            // Kiểm tra ăn mồi
            if (dongMoi == moi.dong && cotMoi == moi.cot)
            {
                // Tính điểm & đặt số lượng đốt lớn lên dựa theo chế độ chơi
                if (cheDoChoi == 1)
                {
                    diemSo += 1;
                    dotTangTruong += 2; // Rắn dài nhanh (tăng thêm 2 ô)
                }
                else if (cheDoChoi == 2)
                {
                    diemSo += 2;
                    dotTangTruong += 1;
                }
                else if (cheDoChoi == 3)
                {
                    diemSo += 3;
                    dotTangTruong += 1;
                }

                SinhMoi();
            }
            else
            {
                // Nếu đang có đốt tăng trưởng chưa mọc hết
                if (dotTangTruong > 0)
                {
                    dotTangTruong--; // Không cắt đuôi để rắn dài ra
                }
                else
                {
                    thanRan.RemoveLast(); // Cắt đuôi bình thường
                }
            }
        }

        static void VeBanDo()
        {
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================================");
            Console.WriteLine("          GAME RẮN SĂN MỒI (SNAKE CONSOLE)       ");
            Console.WriteLine("=================================================");
            Console.ResetColor();

            // Vẽ viền tường phía trên
            Console.Write("+");
            Console.Write(new string('-', chieuRong * 2));
            Console.WriteLine("+");

            // Vẽ nội dung ma trận
            for (int i = 0; i < chieuCao; i++)
            {
                Console.Write("|"); // Tường trái

                for (int j = 0; j < chieuRong; j++)
                {
                    if (thanRan.First!.Value.dong == i && thanRan.First.Value.cot == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O "); // Đầu rắn
                        Console.ResetColor();
                    }
                    else if (moi.dong == i && moi.cot == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("@ "); // Quả táo
                        Console.ResetColor();
                    }
                    else if (chuongNgaiVat.Contains((i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Màu xanh Cyan nổi bật
                        Console.Write("■ "); // Vẽ khối vuông đặc cực kỳ chuyên nghiệp
                        Console.ResetColor();
                    }
                    else if (thanRan.Contains((i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("o "); // Thân rắn
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(". "); // Đất trống
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("|"); // Tường phải
            }

            // Vẽ viền tường phía dưới
            Console.Write("+");
            Console.Write(new string('-', chieuRong * 2));
            Console.WriteLine("+");

            // Hiển thị tên chế độ chơi hiện tại
            string tenCheDo = "";
            string thongTinCheDo = "";

            if (cheDoChoi == 1)
            {
                tenCheDo = "Tự do";
            }
            else if (cheDoChoi == 2)
            {
                tenCheDo = "Bức tường";
            }
            else if (cheDoChoi == 3)
            {
                tenCheDo = "Chướng ngại vật";
                // Đếm ngược thời gian đổi địa hình
                double conLai = thoiGianChuKy - (DateTime.Now - thoiDiemDoiChuongNgaiVat).TotalSeconds;
                if (conLai < 0) conLai = 0;
                thongTinCheDo = $" | ⏳ Đổi địa hình sau: {conLai:F0}s";
            }

            Console.WriteLine($"\n 🏆 Chế độ: {tenCheDo}{thongTinCheDo}");
            Console.WriteLine($" 🍎 Điểm số: {diemSo} | Hướng: {huongDi} | Điều khiển: Arrow/WASD");
            Console.WriteLine(" Nhấn Ctrl + C để thoát game.");
        }

        #region HỆ THỐNG BẢNG XẾP HẠNG TOP 10

        public class KyLucSnake
        {
            public string ten { get; set; } = "";
            public int diem { get; set; }
            public string ngayChoi { get; set; } = "";
        }

        static List<KyLucSnake> DocBangXepHang()
        {
            List<KyLucSnake> ds = new List<KyLucSnake>();
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
                            ds.Add(new KyLucSnake
                            {
                                ten = phan[0],
                                diem = score,
                                ngayChoi = phan[2]
                            });
                        }
                    }
                }
            }
            catch (Exception) { }

            ds.Sort((x, y) => y.diem.CompareTo(x.diem)); // Sắp xếp giảm dần theo điểm
            return ds;
        }

        static void LuuBangXepHang(List<KyLucSnake> ds)
        {
            try
            {
                List<string> dongLuu = new List<string>();
                foreach (var item in ds)
                {
                    dongLuu.Add($"{item.ten}|{item.diem}|{item.ngayChoi}");
                }
                File.WriteAllLines(fileBXH, dongLuu);
            }
            catch (Exception) { }
        }

        static void CapNhatBangXepHang(int diemMoi)
        {
            var bxh = DocBangXepHang();

            if (bxh.Count < 10 || diemMoi > bxh[bxh.Count - 1].diem)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n 🎉 KỶ LỤC MỚI! Điểm số {diemMoi} lọt vào TOP 10 bảng xếp hạng!");
                Console.Write(" Vui lòng nhập tên vinh danh của bạn: ");
                Console.ResetColor();

                string tenNguoiChoi = Console.ReadLine() ?? "VoDanh";
                if (string.IsNullOrWhiteSpace(tenNguoiChoi)) tenNguoiChoi = "VoDanh";

                bxh.Add(new KyLucSnake
                {
                    ten = tenNguoiChoi,
                    diem = diemMoi,
                    ngayChoi = DateTime.Now.ToString("dd/MM/yyyy")
                });

                bxh.Sort((x, y) => y.diem.CompareTo(x.diem));

                if (bxh.Count > 10)
                {
                    bxh.RemoveAt(10);
                }

                LuuBangXepHang(bxh);
            }
        }

        static void HienThiBangXepHang()
        {
            var ds = DocBangXepHang();
            string tenCheDo = cheDoChoi == 1 ? "TỰ DO" : cheDoChoi == 2 ? "BỨC TƯỜNG" : "CHƯỚNG NGẠI VẬT";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n🏆 BẢNG XẾP HẠNG TOP 10 - CHẾ ĐỘ {tenCheDo} 🏆");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"{"Hạng",-6}{"Tên người chơi",-18}{"Điểm số",-15}{"Ngày chơi"}");
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();

            if (ds.Count == 0)
            {
                Console.WriteLine(" Chưa có kỷ lục nào được ghi nhận ở chế độ này.");
            }
            else
            {
                for (int i = 0; i < ds.Count; i++)
                {
                    if (i == 0) Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (i == 1) Console.ForegroundColor = ConsoleColor.Gray;
                    else if (i == 2) Console.ForegroundColor = ConsoleColor.DarkYellow;

                    Console.WriteLine($"{i + 1,-6}{ds[i].ten,-18}{ds[i].diem,-15}{ds[i].ngayChoi}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("-------------------------------------------------");
        }

        static void HienThiMenuBXH()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=================================================");
                Console.WriteLine("               XEM BẢNG XẾP HẠNG                 ");
                Console.WriteLine("=================================================");
                Console.ResetColor();
                Console.WriteLine(" 1. BXH Chế độ Tự do");
                Console.WriteLine(" 2. BXH Chế độ Bức tường");
                Console.WriteLine(" 3. BXH Chế độ Chướng ngại vật");
                Console.WriteLine(" 4. Quay lại Menu chính");
                Console.WriteLine("=================================================");
                Console.Write(" Chọn chế độ để xem BXH (1-4): ");

                string input = Console.ReadLine() ?? "";
                if (input == "4") break;
                if (input == "1" || input == "2" || input == "3")
                {
                    int tempMode = cheDoChoi;
                    cheDoChoi = int.Parse(input);
                    Console.Clear();
                    HienThiBangXepHang();
                    Console.WriteLine("\n Nhấn Enter để quay lại...");
                    Console.ReadLine();
                    cheDoChoi = tempMode; // Trả lại chế độ chơi cũ
                }
            }
        }

        #endregion
    }
}
