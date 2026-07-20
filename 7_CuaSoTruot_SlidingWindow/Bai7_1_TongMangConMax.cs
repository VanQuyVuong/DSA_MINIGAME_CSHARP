using System;
using System.IO;

namespace _7_CuaSoTruot_SlidingWindow
{
    public class Bai7_1_TongMangConMax
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN THỰC TẾ 1: TÌM TỔNG MẢNG CON LỚN NHẤT CÓ ĐỘ DÀI K ---");
            Console.ResetColor();

            // Đọc đề bài trực tiếp từ file txt
            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTTT_Bai7_1_TongMangConMax.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            // Bộ test case mẫu
            var testCases = new (int[] nums, int k, int expected)[]
            {
                (new int[] { 2, 1, 5, 1, 3, 2 }, 3, 9),
                (new int[] { 2, 3, 4, 1, 5 }, 2, 7),
                (new int[] { 1, 2 }, 1, 2)
            };

            bool tatCaThanhCong = true;
            for (int i = 0; i < testCases.Length; i++)
            {
                var tc = testCases[i];
                Console.WriteLine($"\n[Test Case {i + 1}]");
                Console.WriteLine($"Mảng đầu vào: [{string.Join(", ", tc.nums)}], k = {tc.k}");
                
                int ketQua = Giai_TongMangConMaxK(tc.nums, tc.k);
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
                Console.WriteLine("❌ CÓ TEST CASE CHƯA ĐẠT! Hãy cài đặt lại giải thuật bên dưới.");
            }
            Console.ResetColor();

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        /// <summary>
        /// Hãy hoàn thiện giải thuật tìm tổng lớn nhất của mảng con liên tiếp có độ dài K.
        /// Độ phức tạp mong muốn: Thời gian O(N) | Không gian O(1)
        /// </summary>
        public static int Giai_TongMangConMaxK(int[] nums, int k)
        {
            // TODO: Viết code giải thuật của bạn ở đây
            return -1;
        }
    }
}
