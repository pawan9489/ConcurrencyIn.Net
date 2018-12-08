using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;
using System.Collections.Concurrent;

namespace Parallel
{
    class Program
    {
        static int count = 1_000_000_00;
        static IEnumerable<int> numbers = from i in Enumerable.Range(1, count) select i;
        static long total = 0;
        static long num_primes = 0;

        static void Main(string[] args)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            //viaSequential();
            //viaParallel();
            //viaParallelWithLocalState();
            var s = (from i in Enumerable.Range(1, count).AsParallel()
                     .WithDegreeOfParallelism(Math.Max(Environment.ProcessorCount / 2 , 4))
                     where isPrime(i)
                     select (long)i).Sum();

            //var a = Enumerable.Range(1, 10000000)
            //    .Where(i => (i & 1) == 0)
            //    .Select(i => i * 2)
            //    //.Sum(i => (long)i);
            //    .Aggregate(0, (acc, i) => acc + i);
            //WriteLine(a);

            //var nums = Enumerable.Range(0, 100000000);

            //// Create a load-balancing partitioner. Or specify false for static partitioning.
            //Partitioner<int> customPartitioner = Partitioner.Create(nums);

            //// The partitioner is the query's data source.
            //var q = from x in customPartitioner.AsParallel().WithDegreeOfParallelism(2)
            //        select x * Math.PI;

            //var c = 0;
            //q.ForAll((x) =>
            //{
            //    Interlocked.Increment(ref c);
            //});
            //WriteLine(c); // 100000000

            stopWatch.Stop();

            //WriteLine(num_primes); // 78_498
            //WriteLine(total); // 37_550_402_023
            WriteLine(s);
            WriteLine();
            WriteLine($"Elapsed = {stopWatch.Elapsed}");
            WriteLine($"ElapsedTicks = {stopWatch.ElapsedTicks}");
            WriteLine($"ElapsedMilliseconds = {stopWatch.ElapsedMilliseconds}");
            ReadLine();
        }


        static void viaSequential()
        {
            foreach (var number in numbers)
            {
                if (isPrime(number))
                {
                    num_primes++;
                    total += number;
                }
            }
        }

        static void viaParallel()
        {
            System.Threading.Tasks.Parallel.For(1, count + 1, i =>
            {
                if (isPrime(i))
                {
                    Interlocked.Increment(ref num_primes);
                    Interlocked.Add(ref total, i);
                }
            });
        }

        static void viaParallelWithLocalState()
        {
            System.Threading.Tasks.Parallel.For(1, count + 1, 
                () => 0,
                (int i, ParallelLoopState loopState, long tot) => {
                    return isPrime(i) ? tot += i : tot;
                }, 
                value => Interlocked.Add(ref total, value));
        }

        static bool isPrime(int n)
        {
            if (n == 1) return false;
            if (n == 2) return true;
            var boundary = (int)Math.Sqrt(n);
            for (int i = 2; i <= boundary; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }
    }
}
