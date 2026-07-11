using System;
using System.IO;

namespace HaiConTro_TwoPointers
{
    public class Bai10_BeChuaNuocToiDa
    {
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÀI TOÁN THỰC TẾ 4: TỐI ƯU HÓA DUNG TÍCH BỂ NƯỚC MƯA ---");
            Console.ResetColor();

            // Đọc đề bài trực tiếp từ file txt
            string docDeBaiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../BTTT_Bài4_BeChuaNuocToiDa.txt");
            if (File.Exists(docDeBaiPath))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(File.ReadAllText(docDeBaiPath));
                Console.ResetColor();
            }

            Console.WriteLine("-----------------------------------------------------------------");

            int[] height = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
            Console.WriteLine($"Mảng cột đầu vào: [{string.Join(", ", height)}]");

            int ketQua = Giai_ChuaNuocToiDa(height);
            Console.WriteLine($"Dung tích bể nước lớn nhất tính được: {ketQua} m3");
            Console.WriteLine("Kết quả mong đợi: 49 m3");

            if (ketQua == 49)
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

        public static int Giai_ChuaNuocToiDa(int[] height)
        {
            // TODO: Lập trình thuật toán Hai con trỏ tại đây ở commit sau!
            return 0;
        }
    }
}
