using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive.Linq;

namespace ReactiveAppComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            // To test IObservables => create Virtual event stream by converting IEnumerable to IObservable
            var textBox = new TextBox();
            var form = new Form
            {
                Controls = { textBox }
            };

            var msg = Observable.FromEventPattern(textBox, "TextChanged");
            var text = (from e in msg
                       select ((TextBox)e.Sender).Text)
                       .DistinctUntilChanged()
                       .Throttle(TimeSpan.FromSeconds(0.5));

            Console.WriteLine(getN(new Person()));
            Console.WriteLine(Person.func()(4));
            using (text.Subscribe(Console.WriteLine))
            {
                Application.Run(form);
            }
        }

        static int getN(Person p)
        {
            return p?.Name?.Substring(1, 2).Length ?? -1;
        }
    }
    class Person
    {
        public string Name { get; set; }

        public static Func<int, int> func()
        {
            int dbl(int x) => x * 2;
            return dbl;
        }
    }
}
