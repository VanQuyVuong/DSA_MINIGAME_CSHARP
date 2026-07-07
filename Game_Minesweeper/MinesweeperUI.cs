using System;
using System.Collections.Generic;

namespace Game_Minesweeper
{
    public class MinesweeperUI
    {
        public static void VeBanDoHienThi(char[,] banDoHienThi, int kichThuoc)
        {
            try { Console.Clear(); } catch { }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================================");
            Console.WriteLine("            GAME DÒ MÌN (MINESWEEPER)            ");
            Console.WriteLine("=================================================");
            Console.ResetColor();

            // In tiêu đề cột
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    ");
            for (int j = 0; j < kichThuoc; j++)
            {
                Console.Write(j + " ");
            }
            Console.WriteLine("\n  " + new string('-', kichThuoc * 2 + 3));
            Console.ResetColor();

            for (int i = 0; i < kichThuoc; i++)
            {
                // In tiêu đề dòng
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + " | ");
                Console.ResetColor();

                for (int j = 0; j < kichThuoc; j++)
                {
                    char kyTu = banDoHienThi[i, j];

                    if (kyTu == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("# "); // Ô chưa mở
                    }
                    else if (kyTu == 'F')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("F "); // Ô cắm cờ mìn
                    }
                    else if (kyTu == ' ')
                    {
                        Console.Write(". "); // Ô trống đã mở
                    }
                    else
                    {
                        // Các ô số
                        ThietLapMauSacKyTu(kyTu);
                        Console.Write(kyTu + " ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public static void VeBanDoHienLo(int[,] banDoMin, int kichThuoc)
        {
            try { Console.Clear(); } catch { }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=================================================");
            Console.WriteLine("             BẢN ĐỒ MÌN THỰC TẾ                  ");
            Console.WriteLine("=================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    ");
            for (int j = 0; j < kichThuoc; j++)
            {
                Console.Write(j + " ");
            }
            Console.WriteLine("\n  " + new string('-', kichThuoc * 2 + 3));
            Console.ResetColor();

            for (int i = 0; i < kichThuoc; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + " | ");
                Console.ResetColor();

                for (int j = 0; j < kichThuoc; j++)
                {
                    int giaTri = banDoMin[i, j];
                    if (giaTri == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("* "); // Quả mìn phát nổ
                    }
                    else if (giaTri == 0)
                    {
                        Console.Write(". ");
                    }
                    else
                    {
                        ThietLapMauSacKyTu((char)('0' + giaTri));
                        Console.Write(giaTri + " ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Thiết lập màu sắc chuyên nghiệp cho từng con số chỉ định mìn lân cận (1-8)
        /// </summary>
        private static void ThietLapMauSacKyTu(char kyTu)
        {
            switch (kyTu)
            {
                case '1':
                    Console.ForegroundColor = ConsoleColor.Blue; // Số 1: Xanh dương
                    break;
                case '2':
                    Console.ForegroundColor = ConsoleColor.Green; // Số 2: Xanh lá
                    break;
                case '3':
                    Console.ForegroundColor = ConsoleColor.Red; // Số 3: Đỏ
                    break;
                case '4':
                    Console.ForegroundColor = ConsoleColor.DarkBlue; // Số 4: Xanh dương đậm
                    break;
                case '5':
                    Console.ForegroundColor = ConsoleColor.DarkRed; // Số 5: Đỏ đậm
                    break;
                case '6':
                    Console.ForegroundColor = ConsoleColor.DarkCyan; // Số 6: Xanh ngọc đậm
                    break;
                case '7':
                    Console.ForegroundColor = ConsoleColor.DarkGray; // Số 7: Xám đậm
                    break;
                case '8':
                    Console.ForegroundColor = ConsoleColor.Gray; // Số 8: Xám
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }
        }

        /// <summary>
        /// Vẽ bảng xếp hạng Top 10 kỷ lục thời gian giải mìn nhanh nhất
        /// </summary>
        public static void HienThiBangXepHang(List<MinesweeperLogic.KyLucMinesweeper> ds)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n🏆 BẢNG XẾP HẠNG TOP 10 GIẢI MÌN NHANH NHẤT 🏆");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"{"Hạng",-6}{"Tên người chơi",-18}{"Thời gian (s)",-15}{"Ngày chơi"}");
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();

            if (ds.Count == 0)
            {
                Console.WriteLine(" Chưa có kỷ lục nào được ghi nhận ở cấu hình này.");
            }
            else
            {
                for (int i = 0; i < ds.Count; i++)
                {
                    // Màu sắc huy chương cho 3 thứ hạng đầu
                    if (i == 0) Console.ForegroundColor = ConsoleColor.Yellow; // Vàng
                    else if (i == 1) Console.ForegroundColor = ConsoleColor.Gray; // Bạc
                    else if (i == 2) Console.ForegroundColor = ConsoleColor.DarkYellow; // Đồng

                    Console.WriteLine($"{i + 1,-6}{ds[i].ten,-18}{ds[i].thoiGian,-15:F1}{ds[i].ngayChoi}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
