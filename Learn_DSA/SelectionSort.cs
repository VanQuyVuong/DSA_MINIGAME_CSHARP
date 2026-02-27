using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_DSA
{
    internal class SelectionSort
    {
        public static void Run(int[] a)
        {
            int min;
            int n = a.Length;
            for(int i = 0; i <= n-1 ;i++)
            {
                min = i;
                for(int j = i+1; j< n; j++)
                {
                    if( a[j] < a[min])
                    {
                        min = j;
                    }
                }
                int tg = a[i];
                a[i] = a[min];
                a[min] = tg;

                Console.Write($"i={i + 1}: ");
                Console.WriteLine(string.Join(" ", a));
            }
        }
    }
}
