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

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (a[j] < a[j - 1])
                    {
                        int temp = a[j];
                        a[j] = a[j - 1];
                        a[j - 1] = temp;

                        Console.Write($"i={i + 1} j={j + 1}: ");
                        PrintArray(a);
                    }
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