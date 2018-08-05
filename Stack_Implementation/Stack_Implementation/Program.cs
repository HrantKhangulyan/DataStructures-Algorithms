using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> s = new MyStack<int>();
            s.Push(1);
            s.Push(2);
            s.Push(3);
            s.Push(4);
            s.Print();
            Console.ReadLine();
        }
    }
}
