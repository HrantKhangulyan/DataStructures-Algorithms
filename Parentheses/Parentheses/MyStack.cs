using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parentheses
{
    class MyStack<T>
    {
        public LinkedList<T> MyCollection;

        public MyStack()
        {
            MyCollection = new LinkedList<T>();
        }

        public void Push(T obj) //adds an element to the top
        {
            if (MyCollection.Any())
            {
                MyCollection.AddBefore(MyCollection.First, obj);
            }
            else
            {
                MyCollection.AddFirst(obj);
            }
        }

        public T Peek()  // returns the top element
        {
            return MyCollection.First();
        }

        public T Pop() //returns the top element and removes it from stack
        {
            T result = MyCollection.First();
            MyCollection.RemoveFirst();
            return result;
        }

        public bool IsEmpty() //checks if the stack is empty
        {
            return !MyCollection.Any();
        }

        public void Print() //prints the elements of the stack
        {
            if (!MyCollection.Any())
            {
                Console.WriteLine("The stack is empty");
                return;
            }

            Console.WriteLine("The emelents are");
            foreach (var item in MyCollection)
            {
                Console.Write(item + "  ");
            }
        }
    }
}
