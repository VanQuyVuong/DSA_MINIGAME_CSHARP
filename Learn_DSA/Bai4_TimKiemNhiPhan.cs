using System;

namespace Learn_DSA
{
    /*
     * BÀI 4: TÌM KIẾM NHỊ PHÂN (BINARY SEARCH)
     * 
     * Đề bài:
     *   Cho một mảng số nguyên A gồm N phần tử ĐÃ ĐƯỢC SẮP XẾP TĂNG DẦN và số nguyên X.
     *   Hãy tìm vị trí (chỉ số index) của X trong mảng.
     *   Nếu không tìm thấy, trả về -1.
     * 
     * Ràng buộc đặc biệt:
     *   Mảng đầu vào BẮT BUỘC phải được sắp xếp trước. Nếu mảng chưa sắp xếp, thuật toán sẽ trả về kết quả sai lệch.
     * 
     * Hướng giải quyết (Chia để trị):
     *   - Dùng 2 con trỏ: `trai = 0` và `phai = N - 1` để đại diện cho khoảng tìm kiếm hiện tại.
     *   - Trong khi `trai <= phai`:
     *     + Tính chỉ số ở giữa: `giua = trai + (phai - trai) / 2` (dùng công thức này để tránh lỗi tràn số khi `trai` và `phai` quá lớn).
     *     + So sánh A[giua] với X:
     *       * Nếu A[giua] == X: Tìm thấy, trả về `giua`.
     *       * Nếu A[giua] < X: X chắc chắn nằm bên nửa phải -> Thu hẹp phạm vi: `trai = giua + 1`.
     *       * Nếu A[giua] > X: X chắc chắn nằm bên nửa trái -> Thu hẹp phạm vi: `phai = giua - 1`.
     *   - Nếu kết thúc vòng lặp mà không thấy -> Trả về -1.
     * 
     * Độ phức tạp thuật toán:
     *   - Thời gian (Time Complexity): O(log N) vì không gian tìm kiếm giảm một nửa sau mỗi bước so sánh.
     *   - Không gian (Space Complexity): O(1) do chỉ sử dụng các biến trỏ chỉ số đơn giản.
     */
    internal class Bai4_TimKiemNhiPhan
    {
        /// <summary>
        /// Hàm chạy giao diện tương tác và minh họa trace từng bước trên Console
        /// </summary>
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("BÀI 4: TÌM KIẾM NHỊ PHÂN (BINARY SEARCH)");
            Console.WriteLine("Đề bài: Tìm vị trí của X trong mảng đã sắp xếp. Trả về index hoặc -1.");
            Console.WriteLine("Độ phức tạp: Thời gian O(log N) | Không gian O(1)");
            Console.WriteLine("Lưu ý: Mảng BẮT BUỘC phải sắp xếp tăng dần.");
            Console.WriteLine("-----------------------------------------------------------------\n");

            // Mảng mẫu bắt buộc phải sắp xếp tăng dần
            int[] a = { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };
            Console.WriteLine($"Mảng mẫu đã sắp xếp: [{string.Join(", ", a)}]");
            
            Console.Write("Nhập số X cần tìm kiếm: ");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int x))
            {
                Console.WriteLine("Đầu vào không hợp lệ!");
                Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
                Console.ReadLine();
                return;
            }

            // Gọi giải thuật tìm kiếm
            int viTri = TimKiem(a, x, true);

            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine("KẾT QUẢ TÌM KIẾM NHỊ PHÂN:");
            if (viTri != -1)
            {
                Console.WriteLine($"=> Tìm thấy số {x} tại chỉ số index: {viTri}");
            }
            else
            {
                Console.WriteLine($"=> Không tìm thấy số {x} trong mảng. Kết quả: -1");
            }
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
            Console.ReadLine();
        }

        /// <summary>
        /// Logic xử lý cốt lõi của Tìm kiếm nhị phân.
        /// </summary>
        /// <param name="a">Mảng số nguyên đã sắp xếp</param>
        /// <param name="x">Giá trị cần tìm</param>
        /// <param name="isTrace">Có in ra quá trình thu hẹp phạm vi tìm kiếm hay không</param>
        /// <returns>Index của phần tử nếu tìm thấy, ngược lại -1</returns>
        public static int TimKiem(int[] a, int x, bool isTrace = false)
        {
            if (a == null || a.Length == 0)
            {
                return -1;
            }

            int trai = 0;
            int phai = a.Length - 1;
            int step = 1;

            if (isTrace)
            {
                Console.WriteLine("\n--- Quá trình chia đôi không gian tìm kiếm (Trace) ---");
            }

            while (trai <= phai)
            {
                int giua = trai + (phai - trai) / 2;

                if (isTrace)
                {
                    Console.WriteLine($"Bước {step++}:");
                    Console.WriteLine($"  - Phạm vi tìm kiếm hiện tại: chỉ số [{trai} ... {phai}]");
                    Console.WriteLine($"  - Chỉ số ở giữa: giua = {giua} (giá trị A[giua] = {a[giua]})");
                    Console.Write($"  - So sánh A[giua]={a[giua]} với X={x}: ");
                }

                if (a[giua] == x)
                {
                    if (isTrace) Console.WriteLine("→ KHỚP! (Tìm thấy)");
                    return giua;
                }
                else if (a[giua] < x)
                {
                    if (isTrace) Console.WriteLine($"→ {a[giua]} < {x}. Thu hẹp sang NỬA PHẢI.");
                    trai = giua + 1; // Tìm ở nửa bên phải
                }
                else
                {
                    if (isTrace) Console.WriteLine($"→ {a[giua]} > {x}. Thu hẹp sang NỬA TRÁI.");
                    phai = giua - 1; // Tìm ở nửa bên trái
                }
                
                if (isTrace) Console.WriteLine();
            }

            if (isTrace)
            {
                Console.WriteLine("Phạm vi tìm kiếm đã thu hẹp về rỗng (trai > phai) nhưng không tìm thấy X.");
            }

            return -1; // Không tìm thấy
        }
    }
}
