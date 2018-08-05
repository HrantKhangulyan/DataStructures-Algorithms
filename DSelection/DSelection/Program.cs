using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSelection
{
    class Program
    {
        public static int DeterministicSelection(int[] arr, int number)
        {
            int pivotlocation = Patrtitioning(arr, FindMedian(arr));

            if (pivotlocation == number)
            {
                return arr[pivotlocation - 1];
            }

            if (pivotlocation < number)
            {
                return DeterministicSelection(arr.Skip(pivotlocation).ToArray(), pivotlocation - number);
            }
            else
            {
                return DeterministicSelection(arr.Take(pivotlocation).ToArray(), number);
            }
        }

        public static int Patrtitioning(int[] arr, int pivot /*,ref int location*/)
        {
            int CoordinateOfPivot = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == pivot)
                {
                    CoordinateOfPivot = i;
                    //location = i;
                    break;
                }
            }
            Swap(ref arr[0], ref arr[CoordinateOfPivot]);
            int l = 1;
            int p = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[0] <= arr[i])
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
            Swap(ref arr[0], ref arr[p - 1]);
            return p;
        }

        public static int FindMedian(int[] arr)
        {
            int[] MediansArray = new int[(arr.Length + 4) / 5];

            if (arr.Length <= 5)
            {
                if (arr.Length == 2)
                {
                    return Math.Min(arr[0], arr[1]);
                }
                int[] result = arr;
                Array.Sort(result); // ????????????????
                return result[result.Length / 2];
            }

            int[] tmp = { };
            int t = 0;

            for (int i = 0; i < arr.Length; i += 5)
            {
                tmp = arr.Skip(i).Take(5).ToArray();
                if (tmp.Length == 2)
                {
                    MediansArray[t++] = Math.Min(tmp[0], tmp[1]);
                    continue;
                }
                Array.Sort(tmp);
                MediansArray[t++] = tmp[tmp.Length / 2];
            }
            return FindMedian(MediansArray);
        }

        public static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static void Main(string[] args)
        {
            int[] numbers = { 1, -3, 1000, 21, -22, 0, -21 }; // -22 , -21 , -3 , 0 , 1 , 21 ,1000 
            Console.WriteLine(DeterministicSelection(numbers, 4));

            Console.ReadLine();
        }
    }
}
