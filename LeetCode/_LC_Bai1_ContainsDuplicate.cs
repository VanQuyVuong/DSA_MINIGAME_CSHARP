using System;
using System.IO;
using System.Collections.Generic;

namespace LeetCode
{
    public class _LC_Bai1_ContainsDuplicate
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN LEETCODE 217: KIỂM TRA PHẦN TỬ TRÙNG LẶP (CONTAINS DUPLICATE) ---");
            Console.ResetColor();

            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTLC_Bai1_ContainsDuplicate.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            var testCases = new (int[] nums, bool expected)[]
            {
                (new int[] { 1, 2, 3, 1 }, true),
                (new int[] { 1, 2, 3, 4 }, false),
                (new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true),
                (new int[] { 10 }, false)
            };

            bool tatCaThanhCong = true;
            for (int i = 0; i < testCases.Length; i++)
            {
                var tc = testCases[i];
                Console.WriteLine($"\n[Test Case {i + 1}]");
                Console.WriteLine($"Mảng đầu vào: [{string.Join(", ", tc.nums)}]");

                bool ketQua = Giai_ContainsDuplicate(tc.nums);
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

        /// <summary>
        /// THỬ THÁCH CHO BẠN: Viết thuật toán kiểm tra mảng có chứa phần tử trùng lặp hay không.
        /// 
        /// HƯỚNG DẪN CÁC BƯỚC THỰC HIỆN (HINT):
        /// 1. Khởi tạo một Tập hợp băm: var seen = new HashSet<int>();
        /// 2. Duyệt qua từng số 'x' trong mảng 'nums':
        ///    - Kiểm tra: Nếu 'seen.Contains(x)' đúng (số x đã từng xuất hiện) -> trả về true ngay lập tức!
        ///    - Ngược lại: Thêm 'x' vào tập hợp 'seen' bằng lệnh seen.Add(x).
        /// 3. Nếu duyệt hết toàn bộ mảng mà không phát hiện số trùng -> trả về false.
        /// </summary>
        public static bool Giai_ContainsDuplicate(int[] nums)
        {
            // TODO: Bạn hãy tự viết code xử lý thuật toán tại đây nhé!

            return false; // Giá trị trả về mặc định để không bị lỗi biên dịch
        }
    }
}
