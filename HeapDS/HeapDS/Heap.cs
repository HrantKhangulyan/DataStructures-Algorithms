using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapDS
{
    class Heap
    {
        private List<int> Elements;

        public Heap()
        {
            Elements = new List<int>();
        }

        private int RightChildOf(int value)
        {
            if (!RightChildExists(value)) throw new Exception("Element doesnt have right child :( ");
            return Elements[2 * Elements.IndexOf(value) + 2];
        }

        private bool RightChildExists(int value)
        {
            return Elements.Count - 1 >= 2 * Elements.IndexOf(value) + 2;
        }

        private bool LeftChildExists(int value)
        {
            return Elements.Count - 1 >= 2 * Elements.IndexOf(value) + 1;
        }

        private int LeftChildOf(int value)
        {
            if (!LeftChildExists(value)) throw new Exception("Element doesnt have left child :( ");
            return Elements[2 * Elements.IndexOf(value) + 1];
        }

        private int ParentOf(int value)
        {
            return Elements[(Elements.IndexOf(value) * Elements.IndexOf(value)) / (2 * Elements.IndexOf(value) + 1)];
        }

        private void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        public void Add(int value)
        {
            Elements.Add(value);
            while (ParentOf(value) > value)
            {
                int IndexOfParent = Elements.IndexOf(ParentOf(value));
                int IndexOfValue = Elements.IndexOf(value);

                int tmp = Elements[IndexOfParent];
                Elements[IndexOfParent] = Elements[IndexOfValue];
                Elements[IndexOfValue] = tmp;
            }
        }

        public void Remove(int number)
        {
            int index = Elements.IndexOf(number);
            int tmp = Elements[index];
            Elements[index] = Elements[Elements.Count - 1];
            Elements[Elements.Count - 1] = tmp;
            Elements.Remove(Elements.Last());
            int current = Elements[index];

            while (RightChildExists(current) || LeftChildExists(current))
            {
                if (current >= RightChildOf(current) || current >= LeftChildOf(current))
                {
                    if (RightChildExists(current) && LeftChildExists(current))
                    {
                        int indexofmin = Elements.IndexOf(Math.Min(RightChildOf(current), LeftChildOf(current)));
                        int tmp2 = current;
                        Elements[index] = Elements[indexofmin];
                        Elements[indexofmin] = tmp2;
                        index = indexofmin;
                        current = Elements[indexofmin];
                    }
                    if ((!RightChildExists(current) && LeftChildExists(current)) /*&& (LeftChildOf(current) < current)*/)
                    {
                        int indexofleft = Elements.IndexOf(LeftChildOf(current));
                        int tmp2 = current;
                        Elements[index] = Elements[indexofleft];
                        Elements[indexofleft] = tmp2;
                        index = indexofleft;
                        current = Elements[indexofleft];
                    }
                }
            }
        }

        public void PrintElements()
        {
            foreach (var item in Elements)
            {
                Console.Write(item + " ");
            }
        }

        //private bool IsPowerOf2(int x)
        //{
        //    return (x != 0) && (x & (x - 1)) == 0;
        //}

        public int First()
        {
            return Elements.First();
        }
    }
}
