using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapDS
{
    class Program
    {
        public static int FindMedianArr(int[] arr)
        {
            Heap heap = new Heap();
            foreach (var item in arr)
            {
                heap.Add(item);
            }
            for (int i = 0; i < arr.Length / 2; i++)
            {
                heap.Remove(0);
            }
            return heap.First();
        }

        static void Main(string[] args)
        {
            Heap heap = new Heap();
            heap.Add(10);
            heap.Add(14);
            heap.Add(12);
            heap.Add(1);
            heap.Add(5);
            heap.Add(66);
            heap.Add(244);
            heap.Add(133);
            heap.Add(130);
            heap.PrintElements();
            Console.WriteLine("\n-------------------");
            heap.Remove(5);
            heap.PrintElements();
            Console.ReadLine();
        }
    }
}
