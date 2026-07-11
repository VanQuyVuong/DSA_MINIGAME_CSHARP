using System;
using System.Text;

namespace HaiConTro_TwoPointers
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
                Console.WriteLine("        BÀI 10: KỸ THUẬT HAI CON TRỎ (TWO POINTERS PATTERN)      ");
                Console.WriteLine("=================================================================");
                Console.ResetColor();
                Console.WriteLine("   1. Bài toán 1: Đảo ngược chuỗi (LeetCode 344)");
                Console.WriteLine("   2. Bài toán 2: Hai số có tổng bằng Target (LeetCode 167 - Two Sum II)");
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
                        Bai10_DaoNguocChuoi.Run();
                        break;
                    case "2":
                        Bai10_TwoSumSorted.Run();
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
