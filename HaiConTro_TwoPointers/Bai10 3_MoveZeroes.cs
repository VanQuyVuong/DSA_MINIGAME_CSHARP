using System;

namespace HaiConTro_TwoPointers
{
    public class Bai10_MoveZeroes
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN 3: DI CHUYỂN SỐ 0 VỀ CUỐI MẢNG (LEETCODE 283) ---");
            Console.ResetColor();
            Console.WriteLine("Đề bài: Di chuyển tất cả các số 0 về cuối mảng mà vẫn giữ nguyên thứ tự các số khác 0 (in-place).");
            Console.WriteLine("Ý tưởng: Con trỏ 1 (writePointer) giữ vị trí trống, con trỏ 2 (i) chạy nhanh tìm số khác 0 để hoán đổi.");
            Console.WriteLine("-----------------------------------------------------------------");

            int[] nums = { 0, 1, 0, 3, 12 };
            Console.WriteLine($"Mảng ban đầu: [{string.Join(", ", nums)}]");

            Giai_MoveZeroes(nums);

            Console.WriteLine($"Mảng kết quả: [{string.Join(", ", nums)}]");

            int[] expected = { 1, 3, 12, 0, 0 };
            bool check = true;
            for (int i = 0; i < expected.Length; i++)
            {
                if (nums[i] != expected[i]) check = false;
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

        public static void Giai_MoveZeroes(int[] nums)
        {
            int writePointer = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    // Hoán vị phần tử hiện tại với phần tử tại con trỏ chậm
                    int temp = nums[i];
                    nums[i] = nums[writePointer];
                    nums[writePointer] = temp;

                    // Dịch chuyển con trỏ chậm lên 1 vị trí
                    writePointer++;
                }
            }
        }
    }
}
