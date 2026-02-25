using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_DSA
{
    internal class BUBBLESORT
    {
        public static void BubbleSort(int[] a)
        {
            int n = a.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        int temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine(string.Join(" ", a));
        }
    }
}
