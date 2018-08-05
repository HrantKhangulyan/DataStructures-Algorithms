using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        public static void CheckTree(BST tree)
        {
            foreach (var item in tree.Elements)
            {
                Console.WriteLine($"Leftnode of {item?.value} is {item.leftnode?.value}");
                Console.WriteLine($"Rightnode of {item?.value} is {item.rightnode?.value}");
                Console.WriteLine("-----------------------------------------");
            }
        }

        static void Main(string[] args)
        {
            BST tree = new BST();
            tree.Add(20);
            tree.Add(15);
            tree.Add(17);
            tree.Add(30);
            tree.Add(34);
            tree.Add(32);
            tree.Add(10);
            tree.Add(12);
            CheckTree(tree);
            Console.WriteLine("***-*-*-*-*-*-*-***");
            tree.Print();
            tree.Remove(tree.GetNode(15));
            CheckTree(tree);
            Console.WriteLine("***-*-*-*-*-*-*-***");
            tree.Print();
            Console.ReadLine();
        }
    }
}
