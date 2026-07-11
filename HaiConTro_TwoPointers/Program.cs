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
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [Chủ đề luyện tập: Tối ưu hóa Bộ nhớ O(1) & Thời gian O(N)]");
                Console.ResetColor();
                Console.WriteLine("   1. Bài toán 1: Đảo ngược chuỗi (LeetCode 344)");
                Console.WriteLine("   2. Bài toán 2: Hai số có tổng bằng Target (LeetCode 167 - Two Sum II)");
                Console.WriteLine("   3. Bài toán 3: Di chuyển số 0 về cuối mảng (LeetCode 283 - Move Zeroes)");
                Console.WriteLine("   4. Bài toán thực tế 4: Tối ưu bể chứa nước mưa (LeetCode 11)");
                Console.WriteLine("   5. Bài toán thực tế 5: Kiểm tra cụm từ đối xứng (LeetCode 125)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("   0. Thoát chương trình");
                Console.WriteLine("=================================================================");
                Console.Write("Chọn bài toán để giải (0-5): ");

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
                    case "3":
                        Bai10_MoveZeroes.Run();
                        break;
                    case "4":
                        Bai10_BeChuaNuocToiDa.Run();
                        break;
                    case "5":
                        Bai10_ChuoiDoiXuong.Run();
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
