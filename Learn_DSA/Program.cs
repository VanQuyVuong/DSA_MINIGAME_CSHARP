using System;
using System.Text;


namespace Learn_DSA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("Nhập số lượng phần tử:");
            int sl = int.Parse(Console.ReadLine());

            int[] a = new int[sl];

            Console.WriteLine("Nhập các số:");
            for (int i = 0; i < sl; i++)
            {
                Console.WriteLine($"Phần tử {i + 1}:");
                a[i] = int.Parse(Console.ReadLine());
            }

            Console.Clear();

            Console.WriteLine("Chọn thuật toán:");
            Console.WriteLine("1. Sắp xếp nổi bọt");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    BUBBLESORT.BubbleSort(a);
                    break;
            }
        }
    }
}