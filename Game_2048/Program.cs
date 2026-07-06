using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Game_2048
{
    class Program
    {
        // Ma trận game 4x4
        static int[,] banDo = new int[4, 4];
        static int diemSo = 0;
        static int diemCao = 0;
        static string fileDiemCao = "diem_cao.txt";
        static Random rand = new Random();

        static void Main(string[] args)
        {
            // Thiết lập hiển thị tiếng Việt và Unicode
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            while (true)
            {
                KhoiTaoGame();
                bool dangChoi = true;

                while (dangChoi)
                {
                    VeBanDo();

                    if (KiemTraGameOver())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n GAME OVER! Không thể di chuyển được nữa.");
                        Console.ResetColor();
                        Console.Write(" Bạn có muốn chơi lại không? (Y/N): ");
                        
                        while (true)
                        {
                            ConsoleKeyInfo traLoi = Console.ReadKey(true);
                            if (traLoi.Key == ConsoleKey.Y)
                            {
                                dangChoi = false; // Thoát vòng lặp chơi hiện tại, vòng lặp cha sẽ gọi KhoiTaoGame() để bắt đầu lại
                                break;
                            }
                            else if (traLoi.Key == ConsoleKey.N)
                            {
                                Console.WriteLine("\nCảm ơn bạn đã chơi game! Hẹn gặp lại.");
                                return; // Kết thúc toàn bộ chương trình
                            }
                        }
                        continue;
                    }

                    // Đọc phím bấm từ người chơi
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey phim = keyInfo.Key;

                    if (phim == ConsoleKey.Escape)
                    {
                        Console.WriteLine("\nĐang thoát game...");
                        return; // Kết thúc toàn bộ chương trình
                    }

                    // Thực hiện di chuyển
                    bool daDiChuyen = DiChuyen(phim);

                    if (daDiChuyen)
                    {
                        // Nếu có sự thay đổi (di chuyển hoặc gộp ô), sinh thêm một số mới
                        SinhSoNgauNhien();
                    }
                }
            }
        }

        /// <summary>
        /// Khởi tạo game: Đặt toàn bộ bản đồ về 0 và sinh 2 số ngẫu nhiên ban đầu
        /// </summary>
        static void KhoiTaoGame()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    banDo[i, j] = 0;

            diemSo = 0;
            diemCao = DocDiemCao();

            // Sinh 2 số ngẫu nhiên ban đầu
            SinhSoNgauNhien();
            SinhSoNgauNhien();
        }

        /// <summary>
        /// Sinh ngẫu nhiên số 2 (tỷ lệ 90%) hoặc 4 (tỷ lệ 10%) vào một ô trống
        /// </summary>
        static void SinhSoNgauNhien()
        {
            // TODO: Bạn hãy tự viết logic sinh số ngẫu nhiên ở đây!
            // Gợi ý:
            // 1. Tạo danh sách chứa tọa độ các ô trống (có giá trị = 0) dạng Tuple: List<(int dong, int cot)> danhSachOTrong
            // 2. Duyệt ma trận banDo, nếu banDo[i, j] == 0 thì thêm (i, j) vào danh sách.
            // 3. Nếu danh sách không rỗng:
            //    - Chọn ngẫu nhiên một vị trí trong danh sách.
            //    - Gán giá trị tại ô đó là 2 (nếu số ngẫu nhiên từ 0-9 < 9) hoặc 4 (nếu số ngẫu nhiên = 9).



            //Bước 1 Tìm danh sách các ô trống( giá trị =0)
            List<(int dong , int cot)> oTrong = new List<(int , int )>();
            for(int i =0; i<4; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    if (banDo[i,j]== 0)
                    {
                        oTrong.Add((i, j));
                    }
                }
            }

            //Bước 2 :Nếu con ít nhất 1 ô trống, chọn ngẫu nhiên và điền số 
            if (oTrong.Count > 0)
            {
                int viTriNgauNhien = rand.Next(oTrong.Count);
                var oDuocChon = oTrong[viTriNgauNhien];

                // SINH SỐ 2 VỚI TỶ LỆ 90% (NẾU RAND.NEXT(10)<9) NGƯỢC LẠI SINH SỐ 4(10%)
                banDo[oDuocChon.dong, oDuocChon.cot] = rand.Next(10) < 9 ? 2 : 4;
            }
                
        }

        /// <summary>
        /// Dồn tất cả các ô số sang bên TRÁI và thực hiện gộp nếu có các ô cạnh nhau bằng nhau.
        /// </summary>
        /// <returns>True nếu có bất kỳ sự thay đổi nào (dồn hoặc gộp), ngược lại False</returns>
        static bool DonTrai()
        {
            bool daThayDoi = false;

            // TODO: Bạn hãy tự viết thuật toán dồn trái ở đây!
            // Gợi ý thuật toán dồn trái cho mỗi dòng:
            // 1. Dồn các số khác 0 về bên trái (loại bỏ khoảng trống 0).
            //    Ví dụ: [2, 0, 2, 4] -> dồn thành [2, 2, 4, 0]
            // 2. So sánh và gộp các ô cạnh nhau bằng nhau từ trái sang phải:
            //    - Nếu ô i và ô i+1 bằng nhau (và khác 0):
            //      + Ô i = Ô i * 2. Cộng giá trị này vào diemSo.
            //      + Ô i+1 = 0.
            //      Ví dụ: [2, 2, 4, 0] -> gộp thành [4, 0, 4, 0]
            // 3. Dồn các số khác 0 về bên trái một lần nữa sau khi gộp.
            //    Ví dụ: [4, 0, 4, 0] -> dồn thành [4, 4, 0, 0]
            // 4. Nếu trạng thái ma trận trước và sau khi dồn có sự khác biệt, gán daThayDoi = true.


            for(int i =0; i<4; i++)
            {
                //bước 1:dồn các số khác 0 VỀ BÊN TRÁI VÀ DỒN I SANG MẢNG TẠM 'donMoi'
                int[] dongMoi = new int[4];
                int index = 0;
                for(int j =0; j<4; j++)
                {
                    if (banDo[i,j] != 0)
                    {
                        dongMoi[index++] = banDo[i, j];
                    }
                }

                //bước 2 duyệt mảng tạm để duyệt các số giống nhau nằm cạnh nhau 
                for(int j=0; j<3; j++)
                {
                    if (dongMoi[j]!=0 && dongMoi[j] == dongMoi[j + 1])
                    {
                        dongMoi[j] *= 2;
                        diemSo += dongMoi[j];
                        dongMoi[j + 1] = 0;
                        j++;
                    }
                }

                //bước 3 : dồn lại các số khác 0 về bên trái một lần nữa sau khi gộp 
                int[] dongCuoi = new int[4];
                index = 0; 
                for( int j = 0; j <4; j++)
                {
                    if (dongMoi[j] != 0)
                    {
                        dongCuoi[index++] = dongMoi[j];

                    }
                }

                /// Bước 4: Kiểm tra sự thay đổi của dòng, cập nhật kết quả đè lại banDo
                for (int j = 0; j < 4; j++)
                {
                    if (banDo[i, j] != dongCuoi[j])
                    {
                        daThayDoi = true;              // Phát hiện bảng đồ có sự thay đổi
                    }
                    banDo[i, j] = dongCuoi[j];
                }
            }
            return daThayDoi;
        }

        /// <summary>
        /// Xoay ma trận banDo 90 độ theo chiều kim đồng hồ.
        /// </summary>
        static void XoayMaTran90()
        {
            // TODO: Bạn hãy tự viết giải thuật xoay ma trận 90 độ ở đây!
            // Gợi ý:
            // 1. Tạo một ma trận tạm 4x4: int[,] temp = new int[4, 4];
            // 2. Gán temp[j, 3 - i] = banDo[i, j] với mọi i, j từ 0 đến 3.
            // 3. Sao chép ma trận temp đè ngược lại banDo.

            int[,] temp = new int[4, 4];

            //áp dụng công thức xoay ma trận90 độ theo chiều kim đồng hồ 
             for( int i = 0; i<4; i++)
            {
                for(int j =0; j<4; j++){
                    temp[j, 3 - i] = banDo[i, j];

                }
            }

             //Sao chép ma trận tạm temp đè ngược lại ma trận banDo gốc
             for(int i=0; i<4; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    banDo[i, j] = temp[i, j];
                }
            }
        }

        /// <summary>
        /// Quản lý di chuyển theo 4 hướng bằng cách áp dụng xoay ma trận xoay quanh hàm DonTrai()
        /// </summary>
        /// <param name="phim">Phím người chơi nhấn</param>
        /// <returns>True nếu di chuyển thành công, ngược lại False</returns>
        static bool DiChuyen(ConsoleKey phim)
        {
            bool daDiChuyen = false;

            switch (phim)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    daDiChuyen = DonTrai();
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    // Dồn Phải = Xoay 180 độ (xoay 90 độ 2 lần) -> Dồn Trái -> Xoay 180 độ ngược lại
                    XoayMaTran90();
                    XoayMaTran90();
                    daDiChuyen = DonTrai();
                    XoayMaTran90();
                    XoayMaTran90();
                    break;

                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    // Dồn Lên = Xoay 270 độ (hoặc xoay 90 độ 3 lần) -> Dồn Trái -> Xoay 90 độ
                    XoayMaTran90();
                    XoayMaTran90();
                    XoayMaTran90();
                    daDiChuyen = DonTrai();
                    XoayMaTran90();
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    // Dồn Xuống = Xoay 90 độ -> Dồn Trái -> Xoay 270 độ
                    XoayMaTran90();
                    daDiChuyen = DonTrai();
                    XoayMaTran90();
                    XoayMaTran90();
                    XoayMaTran90();
                    break;
            }

            return daDiChuyen;
        }

        /// <summary>
        /// Kiểm tra xem game đã kết thúc chưa.
        /// </summary>
        /// <returns>True nếu không còn ô trống và không gộp được ô nào nữa, ngược lại False</returns>
        static bool KiemTraGameOver()
        {
            // TODO: Bạn hãy tự viết logic kiểm tra Game Over ở đây!
            // Gợi ý:
            // 1. Kiểm tra xem còn ô trống nào không (có giá trị = 0). Nếu còn -> Chưa Game Over (return false).
            // 2. Nếu không còn ô trống, kiểm tra xem có ô cạnh nhau nào có giá trị bằng nhau không (theo hàng ngang hoặc hàng dọc).
            //    Nếu có -> Vẫn có thể gộp được -> Chưa Game Over (return false).
            // 3. Nếu không thuộc hai trường hợp trên -> Game Over (return true).
            

            //bướ 1 :kiểm tra xem còn ô trống nào trống (bằng 0 )không . nếu còn chưa thua
            for(int i =0; i<4; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    if (banDo[i,j] == 0)
                    {
                        return false;
                    }
                }
            }

            //bướ 2 nếu khôgn còn ô trống , kiểm tra xem có ô canh nhau nào bằng nhau khôgn 
            for(int i=0; i<4; i++)
            {
                for(int j =0; j<4; j++)
                {
                    if(j<3 && banDo[i,j] == banDo[i, j + 1])
                    {
                        return false; // còn cộng được chưa thua
                    }

                    if(i<3 && banDo[i,j] == banDo[i+1, j])
                    {
                        return false; // vẫn có thể gộp được chưa thua
                    }
                }
            }

            //bước 3:không có ô trống và không gộp được ô nào nữa => thua cuộc 

            return true;
        }

        /// <summary>
        /// Vẽ giao diện game trực quan bằng màu sắc trên Console
        /// </summary>
        static void VeBanDo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================");
            Console.WriteLine("            GAME 2048            ");
            Console.WriteLine("=================================");
            Console.ResetColor();
            if (diemSo > diemCao)
            {
                diemCao = diemSo;
                LuuDiemCao(diemCao);
            }
            Console.WriteLine($" Điểm số: {diemSo}  |  Kỷ lục: {diemCao}");
            Console.WriteLine("---------------------------------");

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("|      |      |      |      |");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("|");
                    int so = banDo[i, j];
                    if (so == 0)
                    {
                        Console.Write("      ");
                    }
                    else
                    {
                        ThietLapMauSac(so);
                        // In số căn giữa trong khoảng 6 ký tự
                        Console.Write($"{so,4}  ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine("|");
                Console.WriteLine("|______|______|______|______|");
            }

            Console.WriteLine("\n[W, A, S, D] hoặc [Phím Mũi Tên] để di chuyển.");
            Console.WriteLine("[ESC] để thoát.");
        }

        /// <summary>
        /// Thiết lập màu sắc nền và chữ tương ứng với từng con số trong game 2048
        /// </summary>
        static void ThietLapMauSac(int so)
        {
            switch (so)
            {
                case 2:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 16:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case 32:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 64:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 128:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case 256:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case 512:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 1024:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case 2048:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
            }
        }

        /// <summary>
        /// Đọc điểm kỷ lục từ file lưu trữ
        /// </summary>
        static int DocDiemCao()
        {
            try
            {
                if (File.Exists(fileDiemCao))
                {
                    string noiDung = File.ReadAllText(fileDiemCao);
                    if (int.TryParse(noiDung, out int diem))
                    {
                        return diem;
                    }
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi đọc file
            }
            return 0;
        }

        /// <summary>
        /// Lưu điểm kỷ lục mới vào file
        /// </summary>
        static void LuuDiemCao(int diem)
        {
            try
            {
                File.WriteAllText(fileDiemCao, diem.ToString());
            }
            catch (Exception)
            {
                // Bỏ qua lỗi ghi file
            }
        }
    }
}
