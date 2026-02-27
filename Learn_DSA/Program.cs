using System;
using System.Text;

namespace Learn_DSA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Nhập số lượng phần tử:");
            int n = int.Parse(Console.ReadLine());

            int[] original = new int[n];

            Console.WriteLine("Nhập dãy số:");

            for (int i = 0; i < n; i++)
            {
                original[i] = int.Parse(Console.ReadLine());
            }

            while (true)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Bubble Sort");
                Console.WriteLine("2. Selection Sort");
                Console.WriteLine("0. Thoát");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                    break;

                // copy mảng để mỗi thuật toán chạy cùng dữ liệu
                int[] a = (int[])original.Clone();

                Console.WriteLine("\nDãy ban đầu:");
                Console.WriteLine(string.Join(" ", a));

                switch (choice)
                {
                    case 1:
                        BubbleSort.Run(a);
                        break;

                    case 2:
                        SelectionSort.Run(a);
                        break;
                    default:
                        Console.WriteLine("Chưa cài thuật toán này");
                        break;
                }
            }
        }
    }
}