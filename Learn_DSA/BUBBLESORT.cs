using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_DSA
{
    internal class BubbleSort
    {
        public static void Run(int[] a)
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