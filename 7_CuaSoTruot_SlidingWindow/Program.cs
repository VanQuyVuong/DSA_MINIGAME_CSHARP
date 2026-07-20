using System;
using System.Text;

namespace _7_CuaSoTruot_SlidingWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=================================================================");
                Console.WriteLine("        BÀI 7: KỸ THUẬT CỬA SỔ TRƯỢT (SLIDING WINDOW PATTERN)    ");
                Console.WriteLine("=================================================================");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [Chủ đề luyện tập: Tối ưu hóa Bộ nhớ O(1) & Thời gian O(N)]");
                Console.ResetColor();
                Console.WriteLine("   1. Bài toán 1: Tổng mảng con lớn nhất có độ dài K (Fixed Window)");
                Console.WriteLine("   2. Bài toán 2: Mảng con ngắn nhất có tổng >= Target (LeetCode 209)");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("   3. Bài toán 3: [Chưa mở khóa - hoàn thành Bài 2 trước]");
                Console.ResetColor();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("   0. Thoát chương trình");
                Console.WriteLine("=================================================================");
                Console.Write("Chọn bài toán để giải (0-2): ");

                string input = Console.ReadLine() ?? "";
                if (input == "0")
                {
                    Console.WriteLine("\nTạm biệt! Hẹn gặp lại bạn.");
                    break;
                }

                switch (input)
                {
                    case "1":
                        Bai7_1_TongMangConMax.Run();
                        break;
                    case "2":
                        Bai7_2_MangConNganNhat.Run();
                        break;
                    default:
                        Console.WriteLine("\nLựa chọn không hợp lệ! Nhấn phím bất kỳ để tiếp tục...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
