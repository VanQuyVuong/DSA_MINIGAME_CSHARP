using System;
using System.Text;

namespace Learn_DSA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập Encoding để hiển thị tiếng Việt có dấu trên Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================================================");
                Console.WriteLine("        HÀNH TRÌNH CHINH PHỤC CẤU TRÚC DỮ LIỆU & GIẢI THUẬT       ");
                Console.WriteLine("=================================================================");
                Console.WriteLine(" [ NHÓM 1: BÀI TOÁN CƠ BẢN VỀ MẢNG & SỐ HỌC ]");
                Console.WriteLine("   1. Bài 1: Tìm Max/Min & Tính Tổng mảng (Đã học - Click để chạy)");
                Console.WriteLine("   2. Bài 2: Kiểm tra Số Nguyên Tố (Chưa học - Click để xem thông tin)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(" [ NHÓM 2: CÁC THUẬT TOÁN TÌM KIẾM ]");
                Console.WriteLine("   3. Bài 3: Tìm kiếm tuyến tính (Chưa học - Click để xem thông tin)");
                Console.WriteLine("   4. Bài 4: Tìm kiếm nhị phân (Chưa học - Click để xem thông tin)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(" [ NHÓM 3: CÁC THUẬT TOÁN SẮP XẾP ]");
                Console.WriteLine("   5. Bài 5: Sắp xếp nổi bọt (Bubble Sort - Đã có)");
                Console.WriteLine("   6. Bài 6: Sắp xếp chọn (Selection Sort - Đã có)");
                Console.WriteLine("   7. Bài 7: Sắp xếp chèn (Insertion Sort - Đã có)");
                Console.WriteLine("=================================================================");
                Console.WriteLine("   0. Thoát chương trình");
                Console.WriteLine("=================================================================");
                Console.Write("Chọn bài học của bạn (0-7): ");

                string input = Console.ReadLine() ?? "";
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("\nLựa chọn không hợp lệ! Nhấn phím bất kỳ để nhập lại...");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("\nCảm ơn bạn đã sử dụng chương trình. Chúc bạn học tốt DSA!");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        timMaxMinVaTongMang.Run();
                        break;
                    case 2:
                        Console.WriteLine("\n[BÀI 2: KIỂM TRA SỐ NGUYÊN TỐ] - Chúng ta sẽ học bài này ở bước tiếp theo!");
                        Console.WriteLine("Ý tưởng sơ bộ: Kiểm tra tính chia hết từ 2 đến căn bậc hai của N.");
                        Console.WriteLine("\nNhấn Enter để quay lại...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("\n[BÀI 3: TÌM KIẾM TUYẾN TÍNH] - Chúng ta sẽ học bài này ở bước tiếp theo!");
                        Console.WriteLine("Ý tưởng sơ bộ: Duyệt tuần tự qua mảng để tìm khóa X.");
                        Console.WriteLine("\nNhấn Enter để quay lại...");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("\n[BÀI 4: TÌM KIẾM NHỊ PHÂN] - Chúng ta sẽ học bài này ở bước tiếp theo!");
                        Console.WriteLine("Ý tưởng sơ bộ: Tìm kiếm trên mảng đã sắp xếp bằng cách chia đôi khoảng tìm kiếm.");
                        Console.WriteLine("\nNhấn Enter để quay lại...");
                        Console.ReadLine();
                        break;
                    case 5:
                        BubbleSort.Run();
                        break;
                    case 6:
                        SelectionSort.Run();
                        break;
                    case 7:
                        InsertionSort.Run();
                        break;
                    default:
                        Console.WriteLine("\nLựa chọn ngoài danh sách (0-7)! Nhấn phím bất kỳ để tiếp tục...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}