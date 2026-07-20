using System;
using System.IO;
using System.Collections.Generic;

namespace _7_CuaSoTruot_SlidingWindow
{
    public class Bai7_3_ChuoiConDaiNhat
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN THỰC TẾ 3: CHUỖI CON DÀI NHẤT KHÔNG LẶP LẠI KÝ TỰ ---");
            Console.ResetColor();

            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTTT_Bai7_3_ChuoiConDaiNhat.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            var testCases = new (string s, int expected)[]
            {
                ("abcabcbb", 3),
                ("bbbbb", 1),
                ("pwwkew", 3),
                ("", 0)
            };

            bool tatCaThanhCong = true;
            for (int i = 0; i < testCases.Length; i++)
            {
                var tc = testCases[i];
                Console.WriteLine($"\n[Test Case {i + 1}]");
                Console.WriteLine($"Chuỗi đầu vào: \"{tc.s}\"");

                int ketQua = Giai_ChuoiConDaiNhat(tc.s);
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

        public static int Giai_ChuoiConDaiNhat(string s)
        {
            var charSet = new HashSet<char>();
            int left = 0;
            int maxLength = 0;

            for (int right = 0; right < s.Length; right++)
            {
                while (charSet.Contains(s[right]))
                {
                    charSet.Remove(s[left]);
                    left++;
                }
                charSet.Add(s[right]);
                maxLength = Math.Max(maxLength, right - left + 1);
            }

            return maxLength;
        }
    }
}
