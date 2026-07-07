using System;

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
                    else if (kyTu == ' ')
                    {
                        Console.Write(". "); // Ô trống đã mở
                    }
                    else
                    {
                        // Các ô số
                        Console.ForegroundColor = ConsoleColor.Cyan;
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
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(giaTri + " ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
