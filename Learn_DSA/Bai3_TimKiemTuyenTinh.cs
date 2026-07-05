using System;

namespace Learn_DSA
{
    /*
     * BÀI 3: TÌM KIẾM TUYẾN TÍNH (LINEAR SEARCH)
     * 
     * Đề bài:
     *   Cho một mảng số nguyên A gồm N phần tử và số nguyên X.
     *   Hãy tìm vị trí (chỉ số index) đầu tiên của X trong mảng A.
     *   Nếu không tìm thấy X trong mảng, trả về -1.
     * 
     * Hướng giải quyết (Duyệt tuần tự):
     *   Duyệt qua từng phần tử của mảng từ chỉ số 0 đến N-1.
     *   Tại mỗi bước, so sánh phần tử hiện tại A[i] với X.
     *   - Nếu A[i] == X: Trả về ngay chỉ số `i` (tìm thấy).
     *   - Nếu duyệt hết mảng mà không khớp: Trả về -1 (không tìm thấy).
     * 
     * Độ phức tạp thuật toán:
     *   - Thời gian (Time Complexity): 
     *     + Tốt nhất: O(1) khi phần tử cần tìm nằm ở ngay đầu mảng.
     *     + Tệ nhất: O(N) khi phần tử cần tìm ở cuối mảng hoặc không có trong mảng (phải duyệt qua tất cả N phần tử).
     *     + Trung bình: O(N).
     *   - Không gian (Space Complexity): O(1) vì không sử dụng thêm cấu trúc dữ liệu động nào phụ thuộc vào N.
     */
    internal class Bai3_TimKiemTuyenTinh
    {
        /// <summary>
        /// Hàm chạy giao diện tương tác và minh họa trace từng bước trên Console
        /// </summary>
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("BÀI 3: TÌM KIẾM TUYẾN TÍNH (LINEAR SEARCH)");
            Console.WriteLine("Đề bài: Tìm vị trí index đầu tiên của X trong mảng A. Trả về -1 nếu không thấy.");
            Console.WriteLine("Độ phức tạp: Thời gian O(N) | Không gian O(1)");
            Console.WriteLine("-----------------------------------------------------------------\n");

            // Mảng mẫu dùng để tìm kiếm
            int[] a = { 5, 8, 2, 10, 15, 3, 7, 10, 22 };
            Console.WriteLine($"Mảng mẫu hiện có: [{string.Join(", ", a)}]");
            
            Console.Write("Nhập số X cần tìm kiếm: ");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int x))
            {
                Console.WriteLine("Đầu vào không hợp lệ!");
                Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
                Console.ReadLine();
                return;
            }

            // Gọi giải thuật chính để tìm kiếm
            int viTri = TimKiem(a, x, true);

            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine("KẾT QUẢ TÌM KIẾM:");
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
        /// Logic xử lý cốt lõi của Tìm kiếm tuyến tính.
        /// </summary>
        /// <param name="a">Mảng đầu vào</param>
        /// <param name="x">Giá trị cần tìm kiếm</param>
        /// <param name="isTrace">Có in ra các bước so sánh tuần tự không</param>
        /// <returns>Chỉ số index đầu tiên tìm thấy, hoặc -1 nếu không tìm thấy</returns>
        public static int TimKiem(int[] a, int x, bool isTrace = false)
        {
            if (a == null || a.Length == 0)
            {
                return -1;
            }

            if (isTrace)
            {
                Console.WriteLine("\n--- Quá trình tìm kiếm từng bước (Trace) ---");
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (isTrace)
                {
                    Console.Write($"Bước {i + 1}: So sánh A[{i}] = {a[i]} với X = {x} ");
                }

                if (a[i] == x)
                {
                    if (isTrace) Console.WriteLine("→ KHỚP! (Dừng thuật toán)");
                    return i; // Trả về index đầu tiên khớp và dừng luôn
                }

                if (isTrace)
                {
                    Console.WriteLine("→ Không khớp.");
                }
            }

            if (isTrace)
            {
                Console.WriteLine("Đã duyệt hết mảng nhưng không tìm thấy phần tử nào khớp.");
            }

            return -1; // Duyệt hết mảng nhưng không tìm thấy
        }
    }
}
