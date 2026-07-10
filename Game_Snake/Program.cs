using System;
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

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            // Khởi tạo trạng thái ban đầu của rắn
            KhoiTaoRan();

            // Vòng lặp chạy game tự động di chuyển
            while (true)
            {
                VeBanDo();
                DocPhimDieuKhien();
                DiChuyen();

                // Tạm dừng 150ms để tạo hiệu ứng chuyển động mượt mà
                Thread.Sleep(150);
            }
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

            // 3. Cơ chế đi xuyên tường (Nếu chạm tường thì xuất hiện ở phía đối diện)
            dongMoi = (dongMoi + chieuCao) % chieuCao;
            cotMoi = (cotMoi + chieuRong) % chieuRong;

            // 4. Thêm đầu mới vào đầu danh sách (Rắn tiến lên)
            thanRan.AddFirst((dongMoi, cotMoi));

            // 5. Trong Bước 1 chưa có mồi, nên mỗi bước di chuyển rắn phải cắt bỏ đuôi cũ
            thanRan.RemoveLast();
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

            Console.WriteLine($"\n Hướng di chuyển: {huongDi} | Điều khiển bằng phím mũi tên hoặc W, A, S, D.");
            Console.WriteLine(" Nhấn Ctrl + C để thoát game.");
        }
    }
}
