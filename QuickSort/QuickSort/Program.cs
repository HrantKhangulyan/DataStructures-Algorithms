using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace QuickSort
{
    class Program
    {
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static int Pivot(ref int[] arr, int start, int finish)
        {
            if (start - finish == 0) return start;

            int l = start +1;
            int p = start +1;

            for (int i = start + 1; i < finish; i++)
            {
                if (arr[start] <= arr[i])
                {
                    l++;
                }

                else
                {
                    Swap(ref arr[l], ref arr[p]);
                    l++;
                    p++;
                }
            }
            Swap(ref arr[start], ref arr[p - 1]);
            return p - 1;
        }

        public static void QS(ref int[] arr, int start, int finish)
        {
            if (start < finish)
            {
                int position = Pivot(ref arr, start, finish);

                if (start < position)
                {
                    QS(ref arr, start, position);
                }

                if (finish > position)
                {
                    QS(ref arr, position+1 , finish);
                }
            }
        }

        static void Main(string[] args)
        {
            int[] arr = { 0, -1, -3, 12, -112, 1, 2, 66, -111, -121, -2 };
            int[] arr2 = {1,-456,1,1,1,1,7,1,1,1,4};

            QS(ref arr2, 0, arr2.Length);
            foreach (var item in arr2)
            {
                Console.Write(item + " ");
            }
            Console.ReadLine();
        }
    }
}
