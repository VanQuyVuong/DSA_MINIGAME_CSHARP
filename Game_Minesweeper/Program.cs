using System;
using System.Text;

namespace Game_Minesweeper
{
    class Program
    {
        // Kích thước ma trận (Mặc định 9x9)
        static int kichThuoc = 9;
        static int soLuongMin = 10;

        // Ma trận lưu trữ bản đồ mìn thực tế
        // -1: Có mìn
        // 0-8: Số lượng mìn xung quanh ô đó
        static int[,] banDoMin = new int[9, 9];

        static void Main(string[] args)
        {
            // Thiết lập bảng mã Unicode để in được các ký tự đặc biệt
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=================================================");
            Console.WriteLine("          DỰ ÁN 2: GAME MINESWEEPER BƯỚC 1       ");
            Console.WriteLine("=================================================");
            Console.WriteLine("Khởi tạo bản đồ 9x9 và rải ngẫu nhiên 10 quả mìn:");
            Console.WriteLine("-------------------------------------------------");

            KhoiTaoBanDo();
            VeBanDoHienLo();

            Console.WriteLine("\n[Bước 1 Hoàn Thành] Nhấn Enter để thoát...");
            Console.ReadLine();
        }

        /// <summary>
        /// Khởi tạo mảng bản đồ, rải mìn ngẫu nhiên và tính toán số mìn xung quanh mỗi ô
        /// </summary>
        static void KhoiTaoBanDo()
        {
            // 1. Cấp phát động lại ma trận bản đồ
            banDoMin = new int[kichThuoc, kichThuoc];

            // 2. Rải ngẫu nhiên các quả mìn
            Random rand = new Random();
            int minDaDat = 0;
            while (minDaDat < soLuongMin)
            {
                int dong = rand.Next(kichThuoc);
                int cot = rand.Next(kichThuoc);

                // Nếu ô này chưa có mìn
                if (banDoMin[dong, cot] != -1)
                {
                    banDoMin[dong, cot] = -1; // -1 đại diện cho mìn
                    minDaDat++;
                }
            }

            // 3. Tính toán số mìn xung quanh các ô đất trống
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    // Nếu ô hiện tại là mìn, bỏ qua không tính
                    if (banDoMin[i, j] == -1) continue;

                    int đếm = 0;
                    
                    // Duyệt qua 8 ô lân cận (Xung quanh ô i, j)
                    for (int dongXungQuanh = i - 1; dongXungQuanh <= i + 1; dongXungQuanh++)
                    {
                        for (int cotXungQuanh = j - 1; cotXungQuanh <= j + 1; cotXungQuanh++)
                        {
                            // Kiểm tra xem ô lân cận có nằm ngoài biên ma trận không
                            if (dongXungQuanh >= 0 && dongXungQuanh < kichThuoc &&
                                cotXungQuanh >= 0 && cotXungQuanh < kichThuoc)
                            {
                                // Nếu ô lân cận chứa mìn (-1)
                                if (banDoMin[dongXungQuanh, cotXungQuanh] == -1)
                                {
                                    đếm++;
                                }
                            }
                        }
                    }

                    // Lưu số mìn đếm được vào ô hiện tại
                    banDoMin[i, j] = đếm;
                }
            }
        }

        /// <summary>
        /// In bản đồ thực tế ra màn hình để kiểm tra giải thuật (Hiện lộ toàn bộ mìn)
        /// </summary>
        static void VeBanDoHienLo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            // In số thứ tự cột trên cùng
            Console.Write("    ");
            for (int j = 0; j < kichThuoc; j++)
            {
                Console.Write(j + " ");
            }
            Console.WriteLine("\n  " + new string('-', kichThuoc * 2 + 3));
            Console.ResetColor();

            for (int i = 0; i < kichThuoc; i++)
            {
                // In số thứ tự dòng ở đầu dòng
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + " | ");
                Console.ResetColor();

                for (int j = 0; j < kichThuoc; j++)
                {
                    int giaTri = banDoMin[i, j];
                    if (giaTri == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("* "); // In quả mìn
                    }
                    else if (giaTri == 0)
                    {
                        Console.Write(". "); // Ô đất trống hoàn toàn
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(giaTri + " "); // In số mìn xung quanh
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
