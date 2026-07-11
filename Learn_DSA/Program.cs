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
                Console.WriteLine("   1. Bài 1: Tìm Max/Min & Tính Tổng mảng (Đã học)");
                Console.WriteLine("   2. Bài 2: Kiểm tra Số Nguyên Tố (Đã học)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(" [ NHÓM 2: CÁC THUẬT TOÁN TÌM KIẾM ]");
                Console.WriteLine("   3. Bài 3: Tìm kiếm tuyến tính (Đã học)");
                Console.WriteLine("   4. Bài 4: Tìm kiếm nhị phân (Đã học)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(" [ NHÓM 3: CẤU TRÚC DỮ LIỆU ĐỘNG ]");
                Console.WriteLine("   5. Bài 5: Ngăn xếp (Stack) & Hàng đợi (Queue) (Đã học)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(" [ NHÓM 4: ĐỆ QUY & THUẬT TOÁN LOANG ]");
                Console.WriteLine("   6. Bài 6: Đệ quy & Thuật toán Loang (BẠN ĐANG TỰ CODE)");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(" [ NHÓM 5: CÁC THUẬT TOÁN SẮP XẾP ]");
                Console.WriteLine("   7. Bài 7: Sắp xếp nổi bọt (Bubble Sort)");
                Console.WriteLine("   8. Bài 8: Sắp xếp chọn (Selection Sort)");
                Console.WriteLine("   9. Bài 9: Sắp xếp chèn (Insertion Sort)");
                Console.WriteLine("=================================================");
                Console.WriteLine("   0. Thoát chương trình");
                Console.WriteLine("=================================================");
                Console.Write("Chọn bài học của bạn (0-9): ");

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
                        Bai1_TimMaxMinVaTongMang.Run();
                        break;
                    case 2:
                        Bai2_KiemTraSoNguyenTo.Run();
                        break;
                    case 3:
                        Bai3_TimKiemTuyenTinh.Run();
                        break;
                    case 4:
                        Bai4_TimKiemNhiPhan.Run();
                        break;
                    case 5:
                        Bai5_NganXepVaHangDoi.Run();
                        break;
                    case 6:
                        Bai6_DeQuyVaThuatToanLoang.Run();
                        break;
                    case 7:
                        BubbleSort.Run();
                        break;
                    case 8:
                        SelectionSort.Run();
                        break;
                    case 9:
                        InsertionSort.Run();
                        break;
                    default:
                        Console.WriteLine("\nLựa chọn ngoài danh sách (0-9)! Nhấn phím bất kỳ để tiếp tục...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}