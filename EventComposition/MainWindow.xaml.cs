using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EventComposition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int count = 0;
        static DispatcherTimer dispatcherTimer = new DispatcherTimer();
        static EventHandler ev;

        public MainWindow()
        {
            ev = new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Tick += ev;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            InitializeComponent();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Timer.Text = (++count).ToString();
            if(count == 5)
            {
                dispatcherTimer.Tick -= ev;
                dispatcherTimer = null;
                MessageBox.Show("You Lost");
            }
        }

        private void Guess_KeyDown(object sender, KeyEventArgs e)
        {
            var gt = Guess.Text;
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(gt))
            {
                if (gt == "Hey" && dispatcherTimer != null)
                {
                    dispatcherTimer.Tick -= ev;
                    MessageBox.Show("You Won!!. Game Over");
                }
                Guess.Text = "";
            }
        }
    }
}
