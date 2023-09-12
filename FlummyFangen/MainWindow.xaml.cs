using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FlummyFangen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Flummy flummyBall;
        UInt16 GameBals = 0;
        List<Flummy> flummies = new List<Flummy>();
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }
        


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //CreateBals();
            GameLoop_1();
            StartButton.IsEnabled = false;
            SliderBals.IsEnabled = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SolidColorBrush Füllung = new SolidColorBrush();
            Füllung.Color = Colors.Gray;
            double speed = SliderVelo.Value; // Geschwindigkeit aus dem Slider-Wert erhalten

                foreach (Flummy flummy in flummies)
                {
                    flummy.SetSpeed = speed; // Setze die Geschwindigkeit für die Flummy-Instanz
                }

            Spielfeld.Background = Füllung;
        }

        private void SliderBals_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GameBals = (UInt16)SliderBals.Value;
        }

        private async void CreateBals()
        {
            
        }

        public bool RandomBool()
        {
            return random.Next(2) == 0;
        }

        private async void GameLoop_1()
        {
            SolidColorBrush Füllung1 = new SolidColorBrush();
            Füllung1.Color = Colors.Red;
            SolidColorBrush Füllung2 = new SolidColorBrush();
            Füllung2.Color = Colors.Green;

            UInt16 Left = 10;
            UInt16 Top = 20;
            for (UInt16 i = 0; i <= GameBals; i++)
            {
                Flummy flummyInit = new Flummy(Spielfeld, Left, Top, RandomBool(), RandomBool());
                flummies.Add(flummyInit);

                Left += (UInt16)random.Next(20, 120);
                Top += 15;
                await Task.Delay(500);

                Trace.WriteLine(i);
            }


            while (true)
            {
                int randomNumber = random.Next(0, flummies.Count);
                
                int test = flummies.Count;

                //flummies[1].BrushColor = Füllung1;
                //flummies[randomNumber].StartTimer();

                //while (flummies[randomNumber].Timer == null)
                //{
                //    await Task.Delay(100);
                //}

                //if(flummies[randomNumber].StateGame == false)
                //{
                //    foreach (Flummy flummy in flummies)
                //    {
                //        flummy.GridVisibility = Visibility.Collapsed;
                //    }
                //    break;
                //}
                //else
                //{
                //    foreach (Flummy flummy in flummies)
                //    {
                //        flummy.BrushColor = Füllung2;
                //        flummy.SetSpeed += 0.3;
                //    }
                //}
            }
        }
    }

    
}
