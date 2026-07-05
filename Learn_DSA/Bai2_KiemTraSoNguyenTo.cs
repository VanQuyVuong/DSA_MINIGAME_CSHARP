using System;

namespace Learn_DSA
{
    /*
     * BÀI 2: KIỂM TRA SỐ NGUYÊN TỐ (PRIME NUMBER CHECK)
     * 
     * Đề bài:
     *   Cho một số nguyên dương N. Hãy kiểm tra xem N có phải số nguyên tố hay không.
     *   Số nguyên tố là số lớn hơn 1 và chỉ chia hết cho 1 và chính nó.
     * 
     * Cách tiếp cận tối ưu:
     *   - Nếu N <= 1: Không phải số nguyên tố.
     *   - Nếu N = 2 hoặc N = 3: Là số nguyên tố.
     *   - Nếu N chia hết cho 2 hoặc 3: Không phải số nguyên tố.
     *   - Duyệt các ước số lẻ tiềm năng từ 5 đến căn bậc hai của N.
     *     Để tối ưu hơn nữa, ta duyệt bước nhảy i = i + 6 (kiểm tra i và i + 2) 
     *     vì mọi số nguyên tố lớn hơn 3 đều có dạng 6k +/- 1.
     *     Đơn giản hơn cho việc học: Duyệt các số lẻ từ 3 đến căn bậc hai của N với bước nhảy i = i + 2.
     * 
     * Độ phức tạp thuật toán:
     *   - Thời gian (Time Complexity): O(sqrt(N)) vì chỉ lặp tối đa căn bậc hai của N lần.
     *   - Không gian (Space Complexity): O(1) do chỉ dùng các biến đơn lẻ.
     */
    internal class Bai2_KiemTraSoNguyenTo
    {
        /// <summary>
        /// Hàm chạy giao diện tương tác trên Console
        /// </summary>
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("BÀI 2: KIỂM TRA SỐ NGUYÊN TỐ (PRIME CHECK)");
            Console.WriteLine("Đề bài: Cho số nguyên dương N. Kiểm tra N có phải số nguyên tố hay không.");
            Console.WriteLine("Độ phức tạp: Thời gian O(sqrt(N)) | Không gian O(1)");
            Console.WriteLine("-----------------------------------------------------------------\n");

            Console.Write("Nhập số nguyên dương N cần kiểm tra: ");
            string input = Console.ReadLine() ?? "";
            
            if (!long.TryParse(input, out long n) || n < 0)
            {
                Console.WriteLine("Số nhập vào không hợp lệ!");
                Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
                Console.ReadLine();
                return;
            }

            // Gọi thuật toán chính để kiểm tra
            bool laSoNguyenTo = KiemTra(n, true);

            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine("KẾT QUẢ KIỂM TRA:");
            if (laSoNguyenTo)
            {
                Console.WriteLine($"=> Số {n} LÀ SỐ NGUYÊN TỐ.");
            }
            else
            {
                Console.WriteLine($"=> Số {n} KHÔNG PHẢI LÀ SỐ NGUYÊN TỐ.");
            }
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
            Console.ReadLine();
        }

        /// <summary>
        /// Logic xử lý kiểm tra số nguyên tố tối ưu O(sqrt(N))
        /// </summary>
        /// <param name="n">Số cần kiểm tra</param>
        /// <param name="isTrace">Có in ra các bước kiểm tra ước số hay không</param>
        /// <returns>True nếu là số nguyên tố, ngược lại False</returns>
        public static bool KiemTra(long n, bool isTrace = false)
        {
            if (n <= 1)
            {
                if (isTrace) Console.WriteLine($"Phân tích: {n} <= 1 nên KHÔNG PHẢI số nguyên tố.");
                return false;
            }
            if (n == 2)
            {
                if (isTrace) Console.WriteLine("Phân tích: 2 là số nguyên tố chẵn duy nhất.");
                return true;
            }
            if (n % 2 == 0)
            {
                if (isTrace) Console.WriteLine($"Phân tích: {n} là số chẵn lớn hơn 2 (chia hết cho 2) nên KHÔNG PHẢI số nguyên tố.");
                return false;
            }

            long canBacHai = (long)Math.Sqrt(n);
            if (isTrace)
            {
                Console.WriteLine($"Căn bậc hai của {n} làm tròn xuống là: {canBacHai}");
                Console.WriteLine($"Ta sẽ duyệt kiểm tra các ước lẻ i từ 3 đến {canBacHai}:");
            }

            // Chỉ duyệt các số lẻ i = 3, 5, 7, 9...
            for (long i = 3; i <= canBacHai; i += 2)
            {
                if (isTrace) Console.Write($"  Kiểm tra ước i = {i}: ");
                
                if (n % i == 0)
                {
                    if (isTrace) Console.WriteLine($"→ {n} chia hết cho {i}! Phát hiện ước số lẻ.");
                    return false; // Tìm thấy ước số khác -> Không phải số nguyên tố
                }
                
                if (isTrace) Console.WriteLine("→ Không chia hết.");
            }

            if (isTrace) Console.WriteLine("Không tìm thấy ước số nào khác ngoài 1 và chính nó.");
            return true;
        }
    }
}
