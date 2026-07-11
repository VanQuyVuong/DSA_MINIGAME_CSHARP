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
            // TODO: Lập trình thuật toán Hai con trỏ kiểm tra đối xứng tại đây ở commit sau!
            return false;
        }
    }
}
