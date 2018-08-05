using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            List MyList = new List();

            MyList.AddToEnd(4);
            MyList.AddToEnd(55);
            MyList.AddToEnd(7);
            MyList.AddToEnd(1);
            MyList.AddToEnd(0);

            MyList.Remove(4); //mnacacnel karaq stugeq

            MyList.Print();
            Console.ReadLine();
        }
    }
}
