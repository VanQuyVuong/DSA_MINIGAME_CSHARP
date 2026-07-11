using System;
using System.IO;

namespace HaiConTro_TwoPointers
{
    public class Bai10_ChuoiDoiXuong
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN THỰC TẾ 5: KIỂM TRA CỤM TỪ ĐỐI XỨNG ---");
            Console.ResetColor();

            // Đọc đề bài trực tiếp từ file txt
            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTTT_Bài10 5_ChuoiDoiXuong.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            string testInput = "A man, a plan, a canal: Panama";
            Console.WriteLine($"Chuỗi đầu vào: \"{testInput}\"");

            bool ketQua = Giai_KiemTraDoiXuong(testInput);
            Console.WriteLine($"Kết quả kiểm tra của bạn: {ketQua}");
            Console.WriteLine("Kết quả mong đợi: True");

            if (ketQua == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ KẾT QUẢ: THÀNH CÔNG!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ KẾT QUẢ: THẤT BẠI! (Giải thuật chưa cài đặt hoặc sai logic)");
            }
            Console.ResetColor();
            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        public static bool Giai_KiemTraDoiXuong(string s)
        {
            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                // Bỏ qua các ký tự không phải chữ và số ở bên trái
                if (!char.IsLetterOrDigit(s[left]))
                {
                    left++;
                }
                // Bỏ qua các ký tự không phải chữ và số ở bên phải
                else if (!char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }
                // So sánh hai ký tự đã chuyển đổi chữ thường
                else
                {
                    if (char.ToLower(s[left]) != char.ToLower(s[right]))
                    {
                        return false;
                    }
                    left++;
                    right--;
                }
            }

            return true;
        }
    }
}
