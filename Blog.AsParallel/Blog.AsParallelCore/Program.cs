using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.AsParallelCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var list = Enumerable.Range(1, 10000000);

            for (int i = 0; i < 3; i++)
            {
                //Sequential(list);
                Parallel(list);
            }
            //WrongParallel(list);


            Console.ReadLine();
        }

        static void Sequential(IEnumerable<int> list)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var onlyPrimes = list.Where(i => IsPrime(i))
                .Count();
            stopWatch.Stop();

            Console.WriteLine("Seq time:" + stopWatch.Elapsed.TotalMilliseconds);
        }

        static void WrongParallel(IEnumerable<int> list)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var onlyPrimesParallel = list
                .Where(i => IsPrime(i))
                .AsParallel()
                .Count();
            stopWatch.Stop();

            Console.WriteLine("Parallel time:" + stopWatch.Elapsed.TotalSeconds);
        }

        static void Parallel(IEnumerable<int> list)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var onlyPrimesParallel = list
                .AsParallel()
                .Where(i => IsPrime(i))
                .Count();
            stopWatch.Stop();

            Console.WriteLine("Parallel time:" + stopWatch.Elapsed.TotalMilliseconds);
        }

        static bool IsPrime(int candidate)
        {
            // Test whether the parameter is a prime number.
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // Note:
            // ... This version was changed to test the square.
            // ... Original version tested against the square root.
            // ... Also we exclude 1 at the end.
            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }
    }
}
