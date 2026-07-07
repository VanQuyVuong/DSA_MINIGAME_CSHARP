using System;
using System.Text;

namespace Game_Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập bảng mã Unicode hiển thị tiếng Việt
            Console.OutputEncoding = Encoding.UTF8;

            // Khởi tạo đối tượng Logic game (Kích thước 9x9, 10 quả mìn)
            MinesweeperLogic game = new MinesweeperLogic(9, 10);

            bool dangChoi = true;

            while (dangChoi)
            {
                // Vẽ bản đồ che mắt người chơi
                MinesweeperUI.VeBanDoHienThi(game.banDoHienThi, game.kichThuoc);

                // Yêu cầu người chơi nhập tọa độ để mở
                Console.Write("\n Nhập thao tác và tọa độ (M: Mở, F: Cắm cờ. Ví dụ: M 3 4 hoặc F 3 4): ");
                string input = Console.ReadLine() ?? "";
                string[] phan = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Kiểm tra tính hợp lệ của dữ liệu nhập vào (Thao tác + Dòng + Cột)
                if (phan.Length != 3 || 
                    (phan[0].ToUpper() != "M" && phan[0].ToUpper() != "F") ||
                    !int.TryParse(phan[1], out int dong) || 
                    !int.TryParse(phan[2], out int cot) ||
                    dong < 0 || dong >= game.kichThuoc || 
                    cot < 0 || cot >= game.kichThuoc)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Cú pháp không hợp lệ! Vui lòng nhập: [M/F] [Dòng] [Cột] (Ví dụ: M 3 4).");
                    Console.ResetColor();
                    Console.WriteLine(" Nhấn Enter để tiếp tục...");
                    Console.ReadLine();
                    continue;
                }

                string thaoTac = phan[0].ToUpper();

                if (thaoTac == "F")
                {
                    // Thực hiện cắm cờ hoặc gỡ cờ
                    game.CamCo(dong, cot);
                }
                else if (thaoTac == "M")
                {
                    // Thực hiện mở ô
                    bool ketQua = game.MoO(dong, cot);

                    if (!ketQua)
                    {
                        // Trúng mìn -> Game Over!
                        MinesweeperUI.VeBanDoHienLo(game.banDoMin, game.kichThuoc);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n 💥 BÙM!!! Bạn đã giẫm phải mìn! GAME OVER.");
                        Console.ResetColor();
                        dangChoi = false;
                    }
                }

                // Kiểm tra chiến thắng sau mỗi nước đi hợp lệ
                if (dangChoi && game.KiemTraChienThang())
                {
                    MinesweeperUI.VeBanDoHienThi(game.banDoHienThi, game.kichThuoc);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n 🎉 CHÚC MỪNG! Bạn đã tìm ra tất cả các ô an toàn và giành CHIẾN THẮNG!");
                    Console.ResetColor();
                    dangChoi = false;
                }
            }

            Console.WriteLine("\n Nhấn Enter để thoát trò chơi...");
            Console.ReadLine();
        }
    }
}
