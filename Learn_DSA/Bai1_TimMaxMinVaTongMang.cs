using System;

namespace Learn_DSA
{
    /*
     * BÀI 1: TÌM GIÁ TRỊ LỚN NHẤT, NHỎ NHẤT VÀ TÍNH TỔNG MẢNG
     * 
     * Đề bài:
     *   Cho một mảng số nguyên A gồm N phần tử.
     *   Hãy tìm giá trị lớn nhất (Max), nhỏ nhất (Min) và tính tổng (Sum) của tất cả các phần tử.
     * 
     * Cải tiến Kiến trúc (Clean Code):
     *   - Tách biệt logic xử lý giải thuật (`TinhToan`) ra khỏi logic hiển thị màn hình (`Run`).
     *   - Sử dụng ValueTuple `(int max, int min, int tong)` để trả về nhiều kết quả cùng lúc.
     *   - Giúp code dễ viết Unit Test và có thể tái sử dụng ở các môi trường khác (Web, App) thay vì chỉ Console.
     * 
     * Độ phức tạp thuật toán:
     *   - Thời gian (Time Complexity): O(N) duyệt 1 lần (Single Pass).
     *   - Không gian (Space Complexity): O(1) không tốn bộ nhớ bổ sung.
     */
    internal class Bai1_TimMaxMinVaTongMang
    {
        /// <summary>
        /// Hàm chạy giao diện tương tác và minh họa trace từng bước trên Console
        /// </summary>
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("BÀI 1: TÌM GIÁ TRỊ LỚN NHẤT, NHỎ NHẤT VÀ TÍNH TỔNG CỦA MẢNG");
            Console.WriteLine("Đề bài: Cho mảng A có N phần tử. Tìm Max, Min và Tổng các phần tử.");
            Console.WriteLine("Độ phức tạp: Thời gian O(N) | Không gian O(1)");
            Console.WriteLine("-----------------------------------------------------------------\n");

            // Chọn dữ liệu test đầu vào
            Console.WriteLine("Bạn muốn dùng mảng mặc định hay tự nhập?");
            Console.WriteLine("1. Dùng mảng mặc định [12, 3, 5, 27, -4, 9, 15]");
            Console.WriteLine("2. Tự nhập mảng");
            Console.Write("Lựa chọn (1-2): ");
            
            int[] a;
            string choice = Console.ReadLine() ?? "";
            
            if (choice == "2")
            {
                Console.Write("Nhập số lượng phần tử N: ");
                if (!int.TryParse(Console.ReadLine() ?? "", out int n) || n <= 0)
                {
                    Console.WriteLine("Số lượng phần tử không hợp lệ! Dùng mảng mặc định.");
                    a = new int[] { 12, 3, 5, 27, -4, 9, 15 };
                }
                else
                {
                    a = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write($"Nhập A[{i}]: ");
                        if (!int.TryParse(Console.ReadLine() ?? "", out a[i]))
                        {
                            Console.WriteLine("Nhập sai định dạng, đặt mặc định là 0.");
                            a[i] = 0;
                        }
                    }
                }
            }
            else
            {
                a = new int[] { 12, 3, 5, 27, -4, 9, 15 };
            }

            Console.WriteLine($"\nMảng cần xử lý: [{string.Join(", ", a)}]");
            
            // Chạy thuật toán chính và lấy kết quả thông qua Tuple
            var (max, min, tong) = TinhToan(a, true);

            // Hiển thị kết quả cuối cùng
            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine("KẾT QUẢ CUỐI CÙNG:");
            Console.WriteLine($"- Tổng các phần tử (tong)     : {tong}");
            Console.WriteLine($"- Giá trị lớn nhất (max)      : {max}");
            Console.WriteLine($"- Giá trị nhỏ nhất (min)      : {min}");
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
            Console.ReadLine();
        }

        /// <summary>
        /// Logic xử lý cốt lõi của giải thuật.
        /// </summary>
        /// <param name="a">Mảng số nguyên đầu vào</param>
        /// <param name="isTrace">Có hiển thị quá trình chạy từng bước hay không</param>
        /// <returns>Một bộ Tuple gồm (max, min, tổng)</returns>
        public static (int max, int min, int tong) TinhToan(int[] a, bool isTrace = false)
        {
            if (a == null || a.Length == 0)
            {
                throw new ArgumentException("Mảng đầu vào không được rỗng.");
            }

            int tong = 0;
            int max = a[0];
            int min = a[0];

            if (isTrace)
            {
                Console.WriteLine("\n--- Quá trình xử lý từng bước (Trace) ---");
                Console.WriteLine($"Khởi tạo ban đầu: max = {max}, min = {min}, tong = {tong}");
            }

            for (int i = 0; i < a.Length; i++)
            {
                tong += a[i];
                string updatedMax = "";
                string updatedMin = "";

                if (a[i] > max)
                {
                    updatedMax = $" (Cập nhật Max: {max} -> {a[i]})";
                    max = a[i];
                }

                if (a[i] < min)
                {
                    updatedMin = $" (Cập nhật Min: {min} -> {a[i]})";
                    min = a[i];
                }

                if (isTrace)
                {
                    Console.WriteLine($"Bước {i + 1}: Duyệt A[{i}] = {a[i]} | tong = {tong}{updatedMax}{updatedMin}");
                }
            }

            return (max, min, tong);
        }
    }
}
