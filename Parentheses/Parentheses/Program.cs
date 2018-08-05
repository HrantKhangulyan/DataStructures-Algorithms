using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parentheses
{
    class Program
    {
        public static bool IsRightEasy(string input)
        {
            MyStack<char> s = new MyStack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    s.Push(input[i]);
                }

                if (input[i] == ')')
                {
                    s.Pop();
                }
            }

            return s.IsEmpty();
        }

        public static bool IsRightHard(string input)
        {
            if (input.Length == 1) return false;

            MyStack<char> stackforround = new MyStack<char>();
            MyStack<char> stackforsquare = new MyStack<char>();
            MyStack<char> stackfordzev = new MyStack<char>();
            char last = '*';

            for (int i = 0; i < input.Length - 1; i ++)
            {
                switch (input[i])
                {
                    case ('('):
                        stackforround.Push('(');
                        last = '(';
                        break;
                    case ('['):
                        stackforsquare.Push('[');
                        last = '[';
                        break;
                    case ('{'):
                        stackfordzev.Push('{');
                        last = '{';
                        break;
                }

                if ((input[i + 1] == ')' && (!stackforsquare.IsEmpty() || !stackfordzev.IsEmpty())) && last != '(')
                {
                    return false;
                }
                else
                {
                    if (input[i + 1] == ')' && !stackforround.IsEmpty())
                    {
                        stackforround.Pop();
                        continue;
                    }

                    if (stackforround.IsEmpty()) return false;
                }

                if ((input[i + 1] == ']' && (!stackforround.IsEmpty() || !stackfordzev.IsEmpty())) && last != '[')
                {
                    return false;
                }
                else
                {
                    if (input[i + 1] == ']' && !stackforsquare.IsEmpty())
                    {
                        stackforsquare.Pop();
                        continue;
                    }
                    if (stackforsquare.IsEmpty()) return false;
                }

                if ((input[i + 1] == '}' && (!stackforsquare.IsEmpty() || !stackforround.IsEmpty())) && last != '{')
                {
                    return false;
                }
                else
                {
                    if (input[i + 1] == '}' && !stackfordzev.IsEmpty())
                    {
                        stackfordzev.Pop();
                        continue;
                    }

                    if (stackfordzev.IsEmpty()) return false;
                }

            }
            return true;
        }

        static void Main(string[] args)
        {
            //string input = Console.ReadLine();
            //try
            //{
            //    Console.WriteLine(IsRightEasy(input));
            //}
            //catch
            //{
            //    Console.WriteLine("false");
            //}

            string s = Console.ReadLine();
            Console.WriteLine(IsRightHard(s));

            Console.ReadLine();
        }
    }
}
