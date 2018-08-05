using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapDS
{
    class NodeForPQ<T>
    {
        public T value;
        public int PriorityLevel;

        public NodeForPQ(T val , int priorlvl)
        {
            value = val;
            PriorityLevel = priorlvl;
        }
    }
}
