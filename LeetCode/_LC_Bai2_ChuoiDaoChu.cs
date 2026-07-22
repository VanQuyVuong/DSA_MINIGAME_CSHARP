using System;
using System.IO;

namespace LeetCode
{
    public class _LC_Bai2_ChuoiDaoChu
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN LEETCODE 242: KIỂM TRA CHUỖI ĐẢO CHỮ (VALID ANAGRAM) ---");
            Console.ResetColor();

            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTLC_Bai2_ChuoiDaoChu.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            var testCases = new (string s, string t, bool expected)[]
            {
                ("anagram", "nagaram", true),
                ("rat", "car", false),
                ("a", "ab", false),
                ("listen", "silent", true)
            };

            bool tatCaThanhCong = true;
            for (int i = 0; i < testCases.Length; i++)
            {
                var tc = testCases[i];
                Console.WriteLine($"\n[Test Case {i + 1}]");
                Console.WriteLine($"Chuỗi s: \"{tc.s}\" | Chuỗi t: \"{tc.t}\"");

                bool ketQua = Giai_ChuoiDaoChu(tc.s, tc.t);
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
        /// THỬ THÁCH CHO BẠN: Viết thuật toán kiểm tra chuỗi 't' có phải là Anagram của chuỗi 's' hay không.
        /// 
        /// HƯỚNG DẪN CÁC BƯỚC THỰC HIỆN (HINT):
        /// 1. Kiểm tra điều kiện đầu: Nếu s.Length != t.Length -> trả về false ngay lập tức.
        /// 2. Khởi tạo mảng đếm tần suất 26 ký tự: int[] counts = new int[26];
        /// 3. Duyệt i từ 0 đến s.Length - 1:
        ///    - Tăng tần suất ký tự s[i]: counts[s[i] - 'a']++
        ///    - Trừ tần suất ký tự t[i]: counts[t[i] - 'a']--
        /// 4. Duyệt qua mảng counts: Nếu có phần tử nào khác 0 -> trả về false.
        /// 5. Ngược lại -> trả về true.
        /// </summary>
        public static bool Giai_ChuoiDaoChu(string s, string t)
        {
            // TODO: Bạn hãy tự viết code xử lý thuật toán tại đây nhé!


            //bước 1 : nếu độ dài hai chuỗi khác nháu -> trả về false ngay 
            if(s.Length != t.Length)
            {
                return false;
            }
            // bước 2 khởi tạo  mảng đếm tầng suất 26 chữ cái tiếng anh
            int[] counts = new int[26];

            //bước 3 :duyệt qua 2 chuỗi đồng thời 
            for(int i =0; i<s.Length; i++)
            {
                counts[s[i] - 'a']++; //tăng đếm ký tự ở chuỗi s 
                counts[t[i] - 'a']--; //trừ đếm ký tự ở chuỗi t

            }

            // bước 4 kiểm tra xem có ký tự nào bị lệch số lượng hay không 
            foreach(int count in counts)
            {
                if(count != 0)
                {
                    return false;
                }
            }

            return true; // Bước 5: Hai chuỗi là Anagram hoàn hảo
        }
    }
}
