using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace nlognMultiplication
{
    class Program
    {
        public static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            int Xlength = x.ToString().Length;
            int Ylength = y.ToString().Length;

            BigInteger n = (BigInteger)Math.Pow(10, Xlength / 2);
            if (n == 1) return x * y;

            BigInteger a = x / n;
            BigInteger b = x % n; ;

            BigInteger c = y / n;
            BigInteger d = y % n;

            return n * n * Multiply(a, c) + n * (Multiply(a, d) + Multiply(b, c)) + Multiply(b, d);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Multiply(1879,25));
            Console.ReadLine();
        }
    }
}
