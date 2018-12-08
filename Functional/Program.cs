using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
namespace Functional
{
    static class Program
    {

        static Func<float, string> f = a => a.ToString();

        static Func<string, int> g = a => a.Count();

        static Func<float, int> h1 = a => g(f(a));

        static Func<float, int> h2 = f.Compose(g);

        static Func<A, C> Compose<A, B, C>(this Func<A, B> f, Func<B, C> g) => a => g(f(a));

        static int xist => 2;

        static void printList(IEnumerable<int> a)
        {
            foreach (var i in a)
            {
                Write(i + " ");
            }
            WriteLine();
        }

        static void Main(string[] args)
        {
            //var x = h2(12.34f);
            //WriteLine(x);
            //WriteLine(xist);
            //var a = new List<int>();
            //for (int i = 1; i < 100; i++)
            //{
            //    Task.Factory.StartNew(() =>
            //    {
            //        a.Add(Thread.CurrentThread.ManagedThreadId);
            //        WriteLine("{0} - {1}", Thread.CurrentThread.ManagedThreadId, i);
            //    });
            //}

            ////printList(a);
            //ReadLine();
            //printList(a);
            //var b = a.Distinct();
            //printList(b);
            //WriteLine(b.Count());

            var x = from i in Enumerable.Range(1, 10).AsParallel()
                    where (i & 1) == 0
                    from j in Enumerable.Range(1, 10)
                    select (fst: i, snd: j);

            foreach(var i in x)
                WriteLine($"{i.fst} - {i.snd}");


            ReadLine();
        }
    }
}
