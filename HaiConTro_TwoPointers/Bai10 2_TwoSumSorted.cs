using System;

namespace HaiConTro_TwoPointers
{
    public class Bai10_TwoSumSorted
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN 2: HAI SỐ CÓ TỔNG BẰNG TARGET (LEETCODE 167) ---");
            Console.ResetColor();
            Console.WriteLine("Đề bài: Cho mảng numbers đã sắp xếp tăng dần và target. Tìm 2 vị trí (1-indexed) có tổng bằng target.");
            Console.WriteLine("Ý tưởng: Khởi tạo left = 0, right = cuối mảng. Nếu tổng < target thì tăng left, nếu tổng > target thì giảm right.");
            Console.WriteLine("-----------------------------------------------------------------");

            int[] numbers = { 2, 7, 11, 15 };
            int target = 9;
            Console.WriteLine($"Mảng đầu vào: [{string.Join(", ", numbers)}] | Target = {target}");

            int[] result = Giai_TwoSum(numbers, target);

            Console.WriteLine($"Kết quả trả về: [{string.Join(", ", result)}]");

            if (result.Length == 2 && result[0] == 1 && result[1] == 2)
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

        public static int[] Giai_TwoSum(int[] numbers, int target)
        {
            int left = 0;
            int right = numbers.Length - 1;

            while (left < right)
            {
                int tong = numbers[left] + numbers[right];

                if (tong == target)
                {
                    return new int[] { left + 1, right + 1 };
                }
                else if (tong < target)
                {
                    left++; // Tăng con trỏ trái để tổng lớn lên
                }
                else
                {
                    right--; // Giảm con trỏ phải để tổng nhỏ đi
                }
            }

            return new int[0];
        }
    }
}
