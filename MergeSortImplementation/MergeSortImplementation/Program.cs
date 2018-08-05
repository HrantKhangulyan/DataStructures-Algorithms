using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSortImplementation
{
    class Program
    {
        public static int[] MergeSort(int[] arr)
        {
            if (arr.Length == 1) return arr;

            int[] left = arr.Take(arr.Length / 2).ToArray();
            int[] right = arr.Skip(arr.Length / 2).ToArray();
            return Merge(MergeSort(left), MergeSort(right));
        }

        private static int[] Merge(int[] a , int[] b) //Merges Two sorted arrays into one sorted
        {
            int[] result = new int[a.Length + b.Length];

            int index1 = 0;
            int index2 = 0;

            for (int i = 0; i < result.Length; i++)
            {
                if(index1 >= a.Length)
                {
                    result[i] = b[index2++];
                    continue;
                }

                if(index2 >= b.Length)
                {
                    result[i] = a[index1++];
                    continue;
                }

                if(a[index1] <= b[index2])
                {
                    result[i] = a[index1++];
                }
                else
                {
                    result[i] = b[index2++];
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            int[] arr = new int[10];

            for (int i = 0; i < 10; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }

            int[] sorted = MergeSort(arr);

            for (int i = 0; i < sorted.Length; i++)
            {
                Console.Write(sorted[i] + " ");
            }

            Console.ReadLine();
        }
    }
}
