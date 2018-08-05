using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGORITHMS_IncDecArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 1,4,6,8,9,7,5,3,1 };
            Console.WriteLine(F(a));
            Console.ReadLine();
        }

        public static int F(int[] arr)
        {
            if (arr.Length == 1) return arr[0];
            int currpos = arr.Length / 2;
            if(arr[currpos-1] < arr[currpos] && arr[currpos] < arr[currpos + 1])
            {
                return F(arr.Skip(currpos+1).ToArray());
            }
            if (arr[currpos - 1] > arr[currpos] && arr[currpos] > arr[currpos + 1])
            {
                return F(arr.Take(currpos).ToArray());
            }
            if (arr[currpos - 1] < arr[currpos] && arr[currpos] > arr[currpos + 1])
            {
                return arr[currpos];
            }
            return -1;
        }
    }

    
}
