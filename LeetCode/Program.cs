using System;
using System.Text;

namespace LeetCode
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
                Console.WriteLine("          HÀNH TRÌNH CHINH PHỤC CÁC BÀI TOÁN LEETCODE           ");
                Console.WriteLine("=================================================================");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [Dự án tổng hợp LeetCode - Rèn luyện thuật toán & Clean Code]");
                Console.ResetColor();
                Console.WriteLine("   1. Bài 1: Kiểm tra phần tử trùng lặp (LeetCode 217 - Contains Duplicate)");
                Console.WriteLine("   2. Bài 2: Kiểm tra chuỗi đảo chữ (LeetCode 242 - Valid Anagram)");
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
                        _LC_Bai1_ContainsDuplicate.Run();
                        break;
                    case "2":
                        _LC_Bai2_ChuoiDaoChu.Run();
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
