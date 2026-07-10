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
        // Mỗi Node lưu tọa độ (dòng, cột)
        // First Node: Đầu rắn (Head)
        // Last Node: Đuôi rắn (Tail)
        static LinkedList<(int dong, int cot)> thanRan = new LinkedList<(int, int)>();

        // Hướng di chuyển hiện tại của rắn: "UP", "DOWN", "LEFT", "RIGHT"
        static string huongDi = "RIGHT";

        // Tọa độ của mồi và đối tượng sinh số ngẫu nhiên
        static (int dong, int cot) moi;
        static Random rand = new Random();

        // Biến kiểm soát trạng thái trò chơi
        static bool dangChoi = true;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            // Khởi tạo trạng thái ban đầu của rắn
            KhoiTaoRan();

            // Vòng lặp chạy game tự động di chuyển
            while (dangChoi)
            {
                VeBanDo();
                DocPhimDieuKhien();
                DiChuyen();

                // Tạm dừng 150ms để tạo hiệu ứng chuyển động mượt mà
                Thread.Sleep(150);
            }

            // Màn hình hiển thị khi Game Over
            Console.Clear();
            VeBanDo(); // Vẽ lại bản đồ trạng thái cuối cùng
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n 💥 GAME OVER! Rắn đã va chạm tường hoặc tự cắn chính mình!");
            Console.ResetColor();

            int diemCuoi = thanRan.Count - 3;
            Console.WriteLine($" Điểm số của bạn: {diemCuoi}");

            // Cập nhật kỷ lục của người chơi
            CapNhatBangXepHang(diemCuoi);

            // Hiển thị bảng xếp hạng Top 10
            HienThiBangXepHang();

            Console.WriteLine("\n Nhấn Enter để thoát trò chơi...");
            Console.ReadLine();
        }

        /// <summary>
        /// Bắt phím điều khiển từ người chơi (Arrow keys hoặc WASD) - Không chặn luồng
        /// </summary>
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

        /// <summary>
        /// Khởi tạo rắn ban đầu có độ dài là 3 ô nằm ngang
        /// </summary>
        static void KhoiTaoRan()
        {
            thanRan.Clear();
            // Thêm 3 đốt rắn nằm ở dòng 7, từ cột 8 đến 10
            thanRan.AddLast((7, 10)); // Đầu rắn (First Node)
            thanRan.AddLast((7, 9));  // Thân rắn
            thanRan.AddLast((7, 8));  // Đuôi rắn (Last Node)

            // Sinh mồi lần đầu tiên
            SinhMoi();
        }

        /// <summary>
        /// Thuật toán sinh mồi ngẫu nhiên trên bản đồ không trùng lên cơ thể rắn
        /// </summary>
        static void SinhMoi()
        {
            while (true)
            {
                int d = rand.Next(chieuCao);
                int c = rand.Next(chieuRong);

                // Không sinh mồi đè lên cơ thể rắn
                if (!thanRan.Contains((d, c)))
                {
                    moi = (d, c);
                    break;
                }
            }
        }

        /// <summary>
        /// Giải thuật di chuyển của Rắn sử dụng cơ chế Deque (Double-ended queue) trên LinkedList
        /// </summary>
        static void DiChuyen()
        {
            // 1. Lấy tọa độ đầu rắn hiện tại
            var dauHienTai = thanRan.First!.Value;

            // 2. Tính toán tọa độ đầu mới dựa trên hướng đi
            int dongMoi = dauHienTai.dong;
            int cotMoi = dauHienTai.cot;

            switch (huongDi)
            {
                case "UP": dongMoi--; break;
                case "DOWN": dongMoi++; break;
                case "LEFT": cotMoi--; break;
                case "RIGHT": cotMoi++; break;
            }

            // 3. Kiểm tra va chạm tường
            if (dongMoi < 0 || dongMoi >= chieuCao || cotMoi < 0 || cotMoi >= chieuRong)
            {
                dangChoi = false;
                return;
            }

            // 3b. Kiểm tra va chạm thân (tự cắn chính mình)
            if (thanRan.Contains((dongMoi, cotMoi)))
            {
                dangChoi = false;
                return;
            }

            // 4. Thêm đầu mới vào đầu danh sách (Rắn tiến lên)
            thanRan.AddFirst((dongMoi, cotMoi));

            // 5. Kiểm tra nếu đầu mới trùng với vị trí của mồi
            if (dongMoi == moi.dong && cotMoi == moi.cot)
            {
                // Ăn mồi: Không xóa đuôi (rắn dài ra) và sinh mồi mới
                SinhMoi();
            }
            else
            {
                // Di chuyển bình thường: Cắt đuôi cũ ở cuối danh sách
                thanRan.RemoveLast();
            }
        }

        /// <summary>
        /// Vẽ bản đồ game và rắn lên Console
        /// </summary>
        static void VeBanDo()
        {
            // Xóa màn hình nhưng không nháy (bằng cách reset con trỏ về góc 0,0)
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
                    // Kiểm tra xem ô (i, j) có phải là đầu rắn hay không
                    if (thanRan.First!.Value.dong == i && thanRan.First.Value.cot == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O "); // Đầu rắn viết hoa 'O' màu xanh lá
                        Console.ResetColor();
                    }
                    // Kiểm tra xem ô (i, j) có phải là mồi hay không
                    else if (moi.dong == i && moi.cot == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("@ "); // Quả táo mồi màu đỏ nổi bật
                        Console.ResetColor();
                    }
                    // Kiểm tra xem ô (i, j) có phải là thân rắn hay không
                    else if (thanRan.Contains((i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("o "); // Thân rắn viết thường 'o' màu xanh lá tối
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

            Console.WriteLine($"\n 🏆 Điểm số: {thanRan.Count - 3} | Hướng di chuyển: {huongDi} | Điều khiển bằng Arrow/WASD");
            Console.WriteLine(" Nhấn Ctrl + C để thoát game.");
        }

        #region HỆ THỐNG BẢNG XẾP HẠNG TOP 10

        public class KyLucSnake
        {
            public string ten { get; set; } = "";
            public int diem { get; set; }
            public string ngayChoi { get; set; } = "";
        }

        private static string fileBXH = "bxh_snake.txt";

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

            // Sắp xếp giảm dần theo điểm (điểm cao nhất lên đầu)
            ds.Sort((x, y) => y.diem.CompareTo(x.diem));
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

            // Nếu chưa đủ 10 kỷ lục hoặc điểm mới cao hơn kỷ lục thứ 10 hiện tại
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

                // Sắp xếp giảm dần theo điểm
                bxh.Sort((x, y) => y.diem.CompareTo(x.diem));

                // Giữ lại tối đa 10 phần tử
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n🏆 BẢNG XẾP HẠNG TOP 10 KỶ LỤC RẮN SĂN MỒI 🏆");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"{"Hạng",-6}{"Tên người chơi",-18}{"Điểm số",-15}{"Ngày chơi"}");
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();

            if (ds.Count == 0)
            {
                Console.WriteLine(" Chưa có kỷ lục nào được ghi nhận.");
            }
            else
            {
                for (int i = 0; i < ds.Count; i++)
                {
                    // Màu sắc vinh danh 3 thứ hạng đầu
                    if (i == 0) Console.ForegroundColor = ConsoleColor.Yellow; // Vàng
                    else if (i == 1) Console.ForegroundColor = ConsoleColor.Gray; // Bạc
                    else if (i == 2) Console.ForegroundColor = ConsoleColor.DarkYellow; // Đồng

                    Console.WriteLine($"{i + 1,-6}{ds[i].ten,-18}{ds[i].diem,-15}{ds[i].ngayChoi}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("-------------------------------------------------");
        }

        #endregion
    }
}
