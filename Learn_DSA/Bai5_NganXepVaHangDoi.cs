using System;
using System.Collections.Generic;

namespace Learn_DSA
{
    /*
     * BÀI 5: CẤU TRÚC DỮ LIỆU NGĂN XẾP (STACK) & HÀNG ĐỢI (QUEUE)
     * 
     * Đây là bài tập dành cho BẠN tự viết code giải thuật!
     * 
     * --- PHẦN A: NGĂN XẾP (STACK) ---
     * Đề bài: Kiểm tra chuỗi ngoặc hợp lệ (Balanced Parentheses)
     *   Cho một chuỗi chỉ chứa các ký tự ngoặc: '(', ')', '[', ']', '{', '}'.
     *   Hãy kiểm tra xem chuỗi ngoặc này có hợp lệ hay không.
     *   Chuỗi ngoặc hợp lệ khi:
     *   1. Ngoặc mở phải được đóng bởi ngoặc đóng cùng loại.
     *   2. Các ngoặc phải được đóng theo đúng thứ tự (ngoặc mở sau phải đóng trước).
     *   Ví dụ:
     *   - "()[]{}" -> Hợp lệ (True)
     *   - "([{}])" -> Hợp lệ (True)
     *   - "(]"     -> Không hợp lệ (False)
     *   - "([)]"   -> Không hợp lệ (False)
     * 
     * --- PHẦN B: HÀNG ĐỢI (QUEUE) ---
     * Đề bài: Mô phỏng hàng chờ mua vé (Queue Simulation)
     *   Mô phỏng một hàng chờ mua vé xem phim. Khách hàng mới đến sẽ xếp vào cuối hàng (Enqueue).
     *   Quầy vé sẽ phục vụ người ở đầu hàng (Dequeue) và người đó sẽ rời hàng.
     *   Bạn hãy viết code mô phỏng thao tác này.
     */
    internal class Bai5_NganXepVaHangDoi
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=================================================================");
            Console.WriteLine("BÀI 5: CẤU TRÚC DỮ LIỆU NGĂN XẾP (STACK) & HÀNG ĐỢI (QUEUE)");
            Console.WriteLine("=================================================================");
            Console.WriteLine("Chọn phần bài tập bạn muốn chạy thử:");
            Console.WriteLine("1. Phần A: Kiểm tra chuỗi ngoặc hợp lệ (Sử dụng Stack)");
            Console.WriteLine("2. Phần B: Mô phỏng hàng chờ mua vé (Sử dụng Queue)");
            Console.Write("Lựa chọn (1-2): ");
            
            string choice = Console.ReadLine() ?? "";
            if (choice == "1")
            {
                ChayBaiTapStack();
            }
            else if (choice == "2")
            {
                ChayBaiTapQueue();
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ!");
                Console.WriteLine("\nNhấn Enter để quay lại...");
                Console.ReadLine();
            }
        }

        #region PHẦN A: NGĂN XẾP (STACK)

        private static void ChayBaiTapStack()
        {
            Console.Clear();
            Console.WriteLine("--- PHẦN A: KIỂM TRA CHUỖI NGOẶC HỢP LỆ (STACK) ---");
            Console.WriteLine("Nhập chuỗi ngoặc cần kiểm tra (ví dụ: {[()]}) :");
            string input = Console.ReadLine() ?? "";

            bool ketQua = KiemTraNgoacHopLe(input);

            Console.WriteLine("\n-----------------------------------------------------------------");
            if (ketQua)
            {
                Console.WriteLine($"=> Chuỗi '{input}' HỢP LỆ.");
            }
            else
            {
                Console.WriteLine($"=> Chuỗi '{input}' KHÔNG HỢP LỆ.");
            }
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại...");
            Console.ReadLine();
        }

        /// <summary>
        /// Bạn hãy tự viết thuật toán kiểm tra chuỗi ngoặc hợp lệ ở đây!
        /// Gợi ý:
        /// - Tạo một Stack để lưu trữ các ký tự ngoặc mở: Stack<char> nganXep = new Stack<char>();
        /// - Duyệt qua từng ký tự 'c' trong chuỗi:
        ///   + Nếu 'c' là ngoặc mở ('(', '[', '{'), hãy đẩy (Push) nó vào nganXep.
        ///   + Nếu 'c' là ngoặc đóng (')', ']', '}'):
        ///     * Kiểm tra nếu nganXep đang rỗng -> Trả về false ngay (vì không có ngoặc mở tương ứng).
        ///     * Lấy phần tử ở đỉnh (Pop) và kiểm tra xem có khớp cặp với ngoặc đóng 'c' không. Nếu không khớp -> Trả về false.
        /// - Sau khi duyệt hết chuỗi, nếu nganXep rỗng -> Trả về true, ngược lại -> Trả về false (ngoặc mở dư thừa).
        /// </summary>
        public static bool KiemTraNgoacHopLe(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true; // Chuỗi rỗng là hợp lệ
            }

            Stack<char> nganXep = new Stack<char>();

            foreach (char c in s)
            {
                // Nếu là ngoặc mở, đẩy vào ngăn xếp
                if (c == '(' || c == '[' || c == '{')
                {
                    nganXep.Push(c);
                }
                // Nếu là ngoặc đóng
                else if (c == ')' || c == ']' || c == '}')
                {
                    // Nếu gặp ngoặc đóng mà ngăn xếp rỗng -> không khớp cặp
                    if (nganXep.Count == 0)
                    {
                        return false;
                    }

                    char ngoacMoGanNhat = nganXep.Pop();

                    // So sánh sự khớp cặp của ngoặc đóng hiện tại với ngoặc mở ở đỉnh ngăn xếp
                    if (c == ')' && ngoacMoGanNhat != '(') return false;
                    if (c == ']' && ngoacMoGanNhat != '[') return false;
                    if (c == '}' && ngoacMoGanNhat != '{') return false;
                }
            }

            // Nếu kết thúc chuỗi mà ngăn xếp vẫn còn phần tử -> còn ngoặc mở dư thừa
            return nganXep.Count == 0;
        }

        #endregion

        #region PHẦN B: HÀNG ĐỢI (QUEUE)

        private static void ChayBaiTapQueue()
        {
            Console.Clear();
            Console.WriteLine("--- PHẦN B: MÔ PHỎNG HÀNG CHỜ MUA VÉ (QUEUE) ---");
            Console.WriteLine("Khởi tạo hàng chờ...");

            // TODO: Bạn hãy viết code mô phỏng hàng chờ mua vé của bạn ở đây!
            // Ví dụ:
            // 1. Tạo hàng đợi khách hàng: Queue<string> hangCho = new Queue<string>();
            // 2. Thêm khách hàng vào hàng đợi (Enqueue): "Nguyễn Văn A", "Trần Thị B", "Lê Văn C".
            // 3. Hiển thị danh sách người đang xếp hàng.
            // 4. Lần lượt phục vụ khách hàng từ đầu hàng (Dequeue) và in ra màn hình.
            
            Console.WriteLine("\n(Mẹo: Hãy mở file code và tự cài đặt phần này nhé!)");

            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại...");
            Console.ReadLine();
        }

        #endregion
    }
}
