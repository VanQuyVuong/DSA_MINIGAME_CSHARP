using System;
using System.IO;

namespace _7_CuaSoTruot_SlidingWindow
{
    public class Bai7_2_MangConNganNhat
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN THỰC TẾ 2: MẢNG CON NGẮN NHẤT CÓ TỔNG >= TARGET ---");
            Console.ResetColor();

            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTTT_Bai7_2_MangConNganNhat.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            var testCases = new (int[] nums, int target, int expected)[]
            {
                (new int[] { 2, 3, 1, 2, 4, 3 }, 7, 2),
                (new int[] { 1, 4, 4 }, 4, 1),
                (new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 11, 0)
            };

            bool tatCaThanhCong = true;
            for (int i = 0; i < testCases.Length; i++)
            {
                var tc = testCases[i];
                Console.WriteLine($"\n[Test Case {i + 1}]");
                Console.WriteLine($"Mảng đầu vào: [{string.Join(", ", tc.nums)}], target = {tc.target}");

                int ketQua = Giai_MangConNganNhat(tc.nums, tc.target);
                Console.WriteLine($"Kết quả của bạn: {ketQua}");
                Console.WriteLine($"Kết quả mong đợi: {tc.expected}");

                if (ketQua == tc.expected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=> ĐẠT (PASS)");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("=> CHƯA ĐẠT (FAIL)");
                    tatCaThanhCong = false;
                }
                Console.ResetColor();
            }

            Console.WriteLine("\n-----------------------------------------------------------------");
            if (tatCaThanhCong)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ TẤT CẢ CÁC TEST CASES ĐÃ THÀNH CÔNG!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ CÓ TEST CASE CHƯA ĐẠT!");
            }
            Console.ResetColor();

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        public static int Giai_MangConNganNhat(int[] nums, int target)
        {
            int left = 0;
            int currentSum = 0;
            int minLength = int.MaxValue;

            for (int right = 0; right < nums.Length; right++)
            {
                currentSum += nums[right];

                while (currentSum >= target)
                {
                    minLength = Math.Min(minLength, right - left + 1);
                    currentSum -= nums[left];
                    left++;
                }
            }

            return minLength == int.MaxValue ? 0 : minLength;
        }
    }
}
