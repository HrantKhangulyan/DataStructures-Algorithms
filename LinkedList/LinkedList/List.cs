using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class List
    {
        public Node head;

        public List()
        {
            head = null;
        }

        public void Print()
        {
            Node tmp = head;

            if (tmp != null)
            {
                Console.Write("|" + tmp.data + "|->");
            }
            else
            {
                Console.WriteLine("There is no element");
                return;
            }

            while(tmp.next != null)
            {
                tmp = tmp.next;
                Console.Write( "|" + tmp.data + "|->");
            }
        }

        public void AddToEnd(int x)
        {
            Node tmp = head;
            if (head == null)
            {
                head = new Node(x);
            }
            else
            {
                while(tmp.next != null)
                {
                    tmp = tmp.next;
                }

                tmp.next = new Node(x);
            }
        }

        public void AddToBegining(int x)
        {
            Node result = new Node(x);
            result.next = head;
            head = result;
        }

        public void InsertAfter(int x , int number)
        {
            Node tmp = head;
            while(tmp.data != x)
            {
                tmp = tmp.next;
                if (tmp == null) break;
            }
            if (tmp != null)
            {
                Node tmp2 = tmp.next;
                tmp.next = new Node(number);
                tmp.next.next = tmp2;
            }
            else
            {
                Console.WriteLine("Insert corect value ! The list remains ");
            }
        }

        public void Remove(int number)
        {
            int k = 0;
            Node tmp = head;
            Node tmp2 = head;

            while(tmp2 != null)
            {
                if(tmp2.data == number)
                {
                    k++;
                    break;
                }
                else
                {
                    tmp2 = tmp2.next;
                }
            }
            if (k == 0)
            {
                Console.WriteLine("Mentioned value does not exist ! The list is ");
                return;
            }
            if (tmp.data == number)
            {
                head.next = tmp.next;
                head = head.next;
                return;
            }
            while (tmp.next != null && tmp.next.data != number)
            {
                tmp = tmp.next;
                if (tmp == null || tmp.next == null) break;
            }
            if (tmp != null || tmp.next != null)
            { 
                 tmp.next = tmp.next.next;
            }
            
        }

    }
}
