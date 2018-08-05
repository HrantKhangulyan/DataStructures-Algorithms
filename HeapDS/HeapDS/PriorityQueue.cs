using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapDS
{
    class PriorityQueue<T>
    {
        public List<NodeForPQ<T>> Elements;

        public PriorityQueue()
        {
            Elements = new List<NodeForPQ<T>>();
        }

        public void Print()
        {
            foreach (var item in Elements)
            {
                Console.Write(item.value + " ");
            }
            Console.WriteLine();
        }

        public bool IsEmpty()
        {
            return Elements.Count == 0;
        }

        private bool Compare(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        private NodeForPQ<T> GetNode(T val, int plevel)
        {
            foreach (var item in Elements)
            {
                if (Compare(item.value, val) && item.PriorityLevel == plevel)
                {
                    return item;
                }
            }
            throw new Exception("No node matches the given parameters");
        }

        public bool Exists(T val, int plevel)
        {
            foreach (var item in Elements)
            {
                if (Compare(item.value, val) && item.PriorityLevel == plevel)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add(T val, int plevel)
        {
            NodeForPQ<T> node = new NodeForPQ<T>(val, plevel);
            Elements.Add(node);
            int index = Elements.Count - 1;

            for (int i = Elements.Count - 1; i >= 0; i--)
            {
                if (node.PriorityLevel > Elements[i].PriorityLevel)
                {
                    NodeForPQ<T> tmp = Elements[index];
                    Elements[index] = Elements[i];
                    Elements[i] = tmp;
                    index = i;
                }
            }
        }

        public T Peek()
        {
            return Elements.First().value;
        }

        public T Pop()
        {
            T res = Elements.First().value;
            Elements.Remove(Elements.First());
            return res;
        }

        public void Remove(T val, int plevel)
        {
            if (!Exists(val, plevel)) throw new Exception("Node does not exist");
            NodeForPQ<T> node = GetNode(val, plevel);
            Elements.Remove(node);
        }

    }
}
