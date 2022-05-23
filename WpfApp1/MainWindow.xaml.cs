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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer1 = new DispatcherTimer();
        DateTime startTime, pauseTime;
        TimeSpan pauseSpan;
        void timer1_Tick(object sender, EventArgs e)
        {
            if ((bool)checkBox1.IsChecked)
            {
                TimeSpan s = DateTime.Now - startTime - pauseSpan;
                label1.Text = string.Format("{0}:{1}", s.Minutes * 60 + s.Seconds, s.Milliseconds / 100);
            }
            else
                label1.Text = DateTime.Now.ToLongTimeString();
        }
        public MainWindow()
        {
            InitializeComponent();
            timer1.Interval = TimeSpan.FromMilliseconds(1000);
            timer1.Tick += timer1_Tick;
            timer1.Start();
            timer1_Tick(null, null);
            
        }

        public void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox1.IsChecked)
            {
                startTime = DateTime.Now;
                pauseSpan = TimeSpan.Zero;
                timer1.Interval = TimeSpan.FromMilliseconds(100);
            }
            else
            timer1.Interval = TimeSpan.FromMilliseconds(1000);
            timer1_Tick(null, null);
            button1.IsEnabled = button2.IsEnabled = (bool)checkBox1.IsChecked;
            timer1.IsEnabled = true;
        }

       public void button1_Click(object sender, RoutedEventArgs e)
        {
              if (timer1.IsEnabled)
                  pauseSpan += DateTime.Now - pauseTime;
              else
              pauseTime = DateTime.Now;
           // timer1.IsEnabled = !timer1.IsEnabled;

        }

        public void button2_Click(object sender, RoutedEventArgs e)
        {
            timer1.IsEnabled = false;
            pauseTime = startTime;
            pauseSpan = TimeSpan.Zero;
            label1.Text = "0:0";
        }

        public void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                if (!button1.IsEnabled)
                    return;
                if (e.ChangedButton == MouseButton.Left)
                    button1_Click(null, null);
                else
                    if (e.ChangedButton == MouseButton.Right)
                    button2_Click(null, null);
            }
            else
                if (e.ClickCount ==2)
            {
                checkBox1.IsChecked = !(bool)checkBox1.IsChecked;
                checkBox1_Click(null,null);
            }
        }

        
    }
}
