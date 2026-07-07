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
                Console.Write("\n Nhập tọa độ muốn mở (Ví dụ dòng cột: 3 4): ");
                string input = Console.ReadLine() ?? "";
                string[] phan = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Kiểm tra tính hợp lệ của dữ liệu nhập vào
                if (phan.Length != 2 || 
                    !int.TryParse(phan[0], out int dong) || 
                    !int.TryParse(phan[1], out int cot) ||
                    dong < 0 || dong >= game.kichThuoc || 
                    cot < 0 || cot >= game.kichThuoc)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Tọa độ không hợp lệ! Vui lòng nhập lại dòng và cột từ 0 đến 8.");
                    Console.ResetColor();
                    Console.WriteLine(" Nhấn Enter để tiếp tục...");
                    Console.ReadLine();
                    continue;
                }

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

            Console.WriteLine("\n Nhấn Enter để thoát trò chơi...");
            Console.ReadLine();
        }
    }
}
