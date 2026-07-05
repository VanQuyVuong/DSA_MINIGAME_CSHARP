using System;

namespace Learn_DSA
{
    /*
     * BÀI 6: ĐỆ QUY (RECURSION) & THUẬT TOÁN LOANG (FLOOD FILL)
     * 
     * Đây là bài tập dành cho BẠN tự viết code giải thuật!
     * 
     * --- PHẦN A: ĐỆ QUY (RECURSION) ---
     * Đề bài: Tìm số Fibonacci thứ N
     *   Dãy Fibonacci là dãy số bắt đầu bằng 0 và 1, các số sau bằng tổng hai số trước nó:
     *   F(0) = 0, F(1) = 1, F(N) = F(N-1) + F(N-2) với N >= 2.
     *   Hãy viết hàm đệ quy để tìm số Fibonacci thứ N.
     * 
     * --- PHẦN B: THUẬT TOÁN LOANG (FLOOD FILL - DFS) ---
     * Đề bài: Đếm diện tích vùng trống liền kề trong ma trận (Flood Fill)
     *   Cho một ma trận nhị phân 2 chiều kích thước 5x5 đại diện cho một bản đồ.
     *   - Giá trị 0 đại diện cho đất trống (có thể đi qua).
     *   - Giá trị 1 đại diện cho bức tường (không thể đi qua).
     *   Xuất phát từ một điểm (dòng r, cột c) có giá trị 0, hãy dùng thuật toán Loang (Đệ quy DFS) 
     *   để đếm tổng số ô đất trống liền kề nhau (theo 4 hướng: Trên, Dưới, Trái, Phải) liên thông với điểm xuất phát.
     *   
     *   Đây chính là thuật toán cốt lõi để "mở rộng các ô trống" trong Game Dò Mìn (Minesweeper) 
     *   hoặc sơn màu vùng kín trong Paint!
     */
    internal class Bai6_DeQuyVaThuatToanLoang
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=================================================================");
            Console.WriteLine("BÀI 6: ĐỆ QUY (RECURSION) & THUẬT TOÁN LOANG (FLOOD FILL)");
            Console.WriteLine("=================================================================");
            Console.WriteLine("Chọn phần bài tập bạn muốn chạy thử:");
            Console.WriteLine("1. Phần A: Tính số Fibonacci thứ N (Đệ quy)");
            Console.WriteLine("2. Phần B: Đếm diện tích vùng trống (Thuật toán Loang)");
            Console.Write("Lựa chọn (1-2): ");

            string choice = Console.ReadLine() ?? "";
            if (choice == "1")
            {
                ChayBaiTapFibonacci();
            }
            else if (choice == "2")
            {
                ChayBaiTapLoang();
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ!");
                Console.WriteLine("\nNhấn Enter để quay lại...");
                Console.ReadLine();
            }
        }

        #region PHẦN A: ĐỆ QUY (TÍNH SỐ FIBONACCI)

        private static void ChayBaiTapFibonacci()
        {
            Console.Clear();
            Console.WriteLine("--- PHẦN A: TÍNH SỐ FIBONACCI THỨ N ---");
            Console.Write("Nhập chỉ số N (N >= 0): ");
            if (!int.TryParse(Console.ReadLine() ?? "", out int n) || n < 0)
            {
                Console.WriteLine("N không hợp lệ!");
                Console.ReadLine();
                return;
            }

            int ketQua = Fibonacci(n);

            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine($"=> Số Fibonacci thứ {n} là: {ketQua}");
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại...");
            Console.ReadLine();
        }

        /// <summary>
        /// Bạn hãy tự viết hàm đệ quy tính số Fibonacci thứ N ở đây!
        /// Gợi ý:
        /// - Điều kiện dừng (Base case):
        ///   + Nếu n == 0, trả về 0.
        ///   + Nếu n == 1, trả về 1.
        /// - Bước đệ quy (Recursive step):
        ///   + Trả về tổng của hai số Fibonacci đứng trước nó: Fibonacci(n - 1) + Fibonacci(n - 2).
        /// </summary>
        public static int Fibonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        #endregion

        #region PHẦN B: THUẬT TOÁN LOANG (FLOOD FILL - ĐẾM DIỆN TÍCH VÙNG)

        private static void ChayBaiTapLoang()
        {
            Console.Clear();
            Console.WriteLine("--- PHẦN B: ĐẾM DIỆN TÍCH VÙNG TRỐNG (FLOOD FILL) ---");

            // Ma trận bản đồ 5x5
            // 0: Đất trống, 1: Tường
            int[,] banDo = {
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 1, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 1 },
                { 0, 1, 0, 0, 0 }
            };

            // In bản đồ trực quan
            Console.WriteLine("Bản đồ hiện tại (0: đất trống, 1: tường):");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(banDo[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nĐiểm xuất phát: Dòng 0, Cột 0 (Giá trị: " + banDo[0, 0] + ")");
            
            // Mảng đánh dấu các ô đã đi qua để tránh lặp vô hạn
            bool[,] daDiQua = new bool[5, 5];

            // Gọi thuật toán Loang
            int dienTich = Loang(banDo, 0, 0, daDiQua);

            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine($"=> Diện tích vùng trống liên thông với ô (0,0) là: {dienTich} ô");
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại...");
            Console.ReadLine();
        }

        /// <summary>
        /// Bạn hãy viết thuật toán Loang (DFS đệ quy) ở đây để đếm diện tích vùng trống.
        /// Gợi ý:
        /// 1. Điều kiện dừng đệ quy:
        ///    - Nếu chỉ số dong hoặc cot nằm ngoài ma trận (dong < 0, dong >= 5, cot < 0, cot >= 5) -> Trả về 0.
        ///    - Nếu ô hiện tại là tường (banDo[dong, cot] == 1) -> Trả về 0.
        ///    - Nếu ô hiện tại đã được duyệt qua (daDiQua[dong, cot] == true) -> Trả về 0.
        /// 2. Đánh dấu ô hiện tại đã duyệt qua:
        ///    daDiQua[dong, cot] = true;
        /// 3. Gọi đệ quy loang ra 4 hướng (Trên, Dưới, Trái, Phải):
        ///    - Trên:  Loang(banDo, dong - 1, cot, daDiQua)
        ///    - Dưới:  Loang(banDo, dong + 1, cot, daDiQua)
        ///    - Trái:  Loang(banDo, dong, cot - 1, daDiQua)
        ///    - Phải:  Loang(banDo, dong, cot + 1, daDiQua)
        /// 4. Tổng diện tích = 1 (chính ô hiện tại) + tổng diện tích của 4 hướng loang được.
        /// </summary>
        public static int Loang(int[,] banDo, int dong, int cot, bool[,] daDiQua)
        {
            // TODO: Bạn hãy viết code của bạn ở đây!

            return 0; // Thay thế bằng kết quả của bạn
        }

        #endregion
    }
}
