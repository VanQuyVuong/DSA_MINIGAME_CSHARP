using System;

namespace HaiConTro_TwoPointers
{
    public class Bai10_DaoNguocChuoi
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN 1: ĐẢO NGƯỢC CHUỖI (LEETCODE 344) ---");
            Console.ResetColor();
            Console.WriteLine("Đề bài: Đảo ngược mảng kí tự s tại chỗ (in-place) với bộ nhớ O(1).");
            Console.WriteLine("Ý tưởng: Dùng con trỏ trái (left) ở đầu, con trỏ phải (right) ở cuối, hoán vị và tịnh tiến vào giữa.");
            Console.WriteLine("-----------------------------------------------------------------");

            char[] testInput = { 'h', 'e', 'l', 'l', 'o' };
            Console.WriteLine($"Mảng ban đầu: [{string.Join(", ", testInput)}]");

            // Thực thi thuật toán
            Giai_DaoNguocChuoi(testInput);

            Console.WriteLine($"Mảng sau khi đảo: [{string.Join(", ", testInput)}]");
            
            // Validate kết quả
            char[] expected = { 'o', 'l', 'l', 'e', 'h' };
            bool check = true;
            for(int i = 0; i < expected.Length; i++)
            {
                if (testInput[i] != expected[i]) check = false;
            }

            if (check)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ KẾT QUẢ: THÀNH CÔNG! Giải thuật của bạn chạy đúng.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ KẾT QUẢ: THẤT BẠI! Hãy kiểm tra lại logic thuật toán.");
            }
            Console.ResetColor();
            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        public static void Giai_DaoNguocChuoi(char[] s)
        {
            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                // Hoán đổi vị trí hai ký tự
                char temp = s[left];
                s[left] = s[right];
                s[right] = temp;

                // Dịch chuyển hai con trỏ xích lại gần nhau
                left++;
                right--;
            }
        }
    }
}
