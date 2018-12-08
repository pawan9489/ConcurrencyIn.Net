using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CoRoutines
{
    class Program
    {
        static string[] sentences = new string[] {
            "Hi, How are you?",
            "Fine, thank you!",
            "Nothing much",
            "Let's Party",
            "Bye!"
        };
        static Random rand = new Random();

        // Scala Iteratees == Python Yield == Enumerable and Observable at same time
        // Pull and Push at the same time

        static void Main(string[] args)
        {
            var s = sender();
            var r = receiver();
            while(true)
            {
                try
                {
                    r(s());
                } catch(IndexOutOfRangeException e)
                {
                    WriteLine($"End Of Communication Since -> {e.Message}");
                    break;
                }
            }
            ReadLine();
        }

        static Func<string> sender()
        {
            var len = sentences.Length;
            Func<string> helper = () => sentences[rand.Next(0, len)];
            return helper;
        }

        static Action<string> receiver()
        {
            var len = sentences.Length;
            Action<string> helper = (recv) =>
            {
                WriteLine($"Received - {recv}");
                if(recv == "Bye!")
                    throw new IndexOutOfRangeException();
                var reply = sentences[rand.Next(0, len - 1)]; // Never reply as Bye
                WriteLine($"Replied - {reply}");
            };
            return helper;
        }
    }
}
