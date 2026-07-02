using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_DSA
{
    /*
     * BÀI 5: SẮP XẾP NỔI BỌT (BUBBLE SORT)
     * 
     * Đề bài:
     *   Cho mảng số nguyên gồm N phần tử. Hãy sắp xếp mảng theo thứ tự tăng dần.
     * 
     * Độ phức tạp:
     *   - Thời gian (Time Complexity): 
     *     + Tệ nhất & Trung bình: O(N^2) khi các cặp phần tử cần so sánh lặp liên tục.
     *     + Tốt nhất: O(N) nếu mảng đã được sắp xếp sẵn (và thuật toán có tối ưu flag check).
     *   - Không gian (Space Complexity): O(1) do sắp xếp tại chỗ (in-place).
     * 
     * Ý tưởng:
     *   - Đi từ cuối mảng về đầu mảng, so sánh hai phần tử kế cận, nếu chúng ngược thứ tự thì hoán đổi.
     *   - Qua mỗi lượt duyệt, phần tử nhỏ nhất sẽ "nổi lên" đầu mảng giống như bọt khí.
     */
    internal class BubbleSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=================================================================");
            Console.WriteLine("BÀI 5: SẮP XẾP NỔI BỌT (BUBBLE SORT)");
            Console.WriteLine("=================================================================");
            Console.WriteLine("Đề bài: Sắp xếp mảng tăng dần bằng cách nổi bọt các phần tử nhỏ hơn.");
            Console.WriteLine("Độ phức tạp: Thời gian O(N^2) | Không gian O(1)");
            Console.WriteLine("-----------------------------------------------------------------\n");

            Console.WriteLine("Bạn muốn dùng mảng mặc định hay tự nhập?");
            Console.WriteLine("1. Dùng mảng mặc định [5, 3, 8, 2, 1]");
            Console.WriteLine("2. Tự nhập mảng");
            Console.Write("Lựa chọn (1-2): ");

            int[] a;
            string choice = Console.ReadLine() ?? "";
            if (choice == "2")
            {
                Console.Write("Nhập số lượng phần tử N: ");
                if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
                {
                    Console.WriteLine("Số lượng không hợp lệ! Dùng mảng mặc định.");
                    a = new int[] { 5, 3, 8, 2, 1 };
                }
                else
                {
                    a = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write($"Nhập A[{i}]: ");
                        if (!int.TryParse(Console.ReadLine(), out a[i]))
                        {
                            a[i] = 0;
                        }
                    }
                }
            }
            else
            {
                a = new int[] { 5, 3, 8, 2, 1 };
            }

            Console.WriteLine($"\nMảng ban đầu: [{string.Join(" ", a)}]");
            
            // Gọi hàm sắp xếp nguyên bản của bạn
            RunSort(a);

            Console.WriteLine("=================================================================");
            Console.WriteLine("\nNhấn Enter để quay lại Menu chính...");
            Console.ReadLine();
        }

        // Logic sắp xếp gốc do bạn tự build
        private static void RunSort(int[] a)
        {
            int n = a.Length;

            Console.WriteLine("\nQuá trình sắp xếp:");

            for (int i = 1; i <= n - 1; i++)
            {
                for (int j = n - 1; j >= i; j--)
                {
                    int realJ = j;

                    Console.WriteLine(
                        $"So sánh a[{j}]={a[realJ]} và a[{j - 1}]={a[realJ - 1]}"
                    );

                    if (a[realJ] < a[realJ - 1])
                    {
                        int temp = a[realJ];
                        a[realJ] = a[realJ - 1];
                        a[realJ - 1] = temp;

                        Console.WriteLine("→ Có đổi chỗ");
                    }
                    else
                    {
                        Console.WriteLine("→ Không đổi");
                    }

                    Console.Write($"i={i} j={j}: ");
                    PrintArray(a);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nDãy sau khi sắp xếp:");
            PrintArray(a);
        }

        static void PrintArray(int[] a)
        {
            Console.WriteLine(string.Join(" ", a));
        }
    }
}