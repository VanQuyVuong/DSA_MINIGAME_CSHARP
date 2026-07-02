using System;

namespace Learn_DSA
{
    /*
     * BÀI 7: SẮP XẾP CHÈN (INSERTION SORT)
     * 
     * Đề bài:
     *   Cho mảng số nguyên gồm N phần tử. Hãy sắp xếp mảng theo thứ tự tăng dần.
     * 
     * Độ phức tạp:
     *   - Thời gian (Time Complexity):
     *     + Tốt nhất: O(N) khi mảng đã được sắp xếp sẵn.
     *     + Tệ nhất & Trung bình: O(N^2) khi mảng bị ngược thứ tự hoàn toàn.
     *   - Không gian (Space Complexity): O(1) do sắp xếp tại chỗ.
     * 
     * Ý tưởng:
     *   - Duyệt qua từng phần tử và "chèn" nó vào vị trí thích hợp trong đoạn mảng đã sắp xếp phía trước nó.
     *   - Tương tự như cách ta sắp xếp các quân bài trên tay.
     */
    internal class InsertionSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=================================================================");
            Console.WriteLine("BÀI 7: SẮP XẾP CHÈN (INSERTION SORT)");
            Console.WriteLine("=================================================================");
            Console.WriteLine("Đề bài: Sắp xếp mảng tăng dần bằng cách chèn phần tử vào đoạn đã xếp.");
            Console.WriteLine("Độ phức tạp: Thời gian O(N^2) | Không gian O(1)");
            Console.WriteLine("-----------------------------------------------------------------\n");

            Console.WriteLine("Bạn muốn dùng mảng mặc định hay tự nhập?");
            Console.WriteLine("1. Dùng mảng mặc định [12, 11, 13, 5, 6]");
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
                    a = new int[] { 12, 11, 13, 5, 6 };
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
                a = new int[] { 12, 11, 13, 5, 6 };
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
            Console.WriteLine("\nQuá trình sắp xếp (Insertion Sort):");

            for (int i = 1; i < n; i++)
            {
                int key = a[i];
                int j = i - 1;

                Console.WriteLine($"\nChèn phần tử a[{i}]={key} vào mảng đã sắp xếp:");

                while (j >= 0 && a[j] > key)
                {
                    Console.WriteLine($"  a[{j}]={a[j]} > {key}, dời a[{j}] sang phải");
                    a[j + 1] = a[j];
                    j = j - 1;
                }
                a[j + 1] = key;
                
                Console.Write($"Kết quả sau bước i={i}: ");
                Console.WriteLine(string.Join(" ", a));
            }

            Console.WriteLine("\nDãy sau khi sắp xếp:");
            Console.WriteLine(string.Join(" ", a));
        }
    }
}
