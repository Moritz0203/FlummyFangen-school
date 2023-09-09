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


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateBals();
            button1.IsEnabled = false;
            SliderBals.IsEnabled = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SolidColorBrush Füllung = new SolidColorBrush();
            Füllung.Color = Colors.Gray;
            if (flummyBall != null)
            {
                double speed = SliderVelo.Value; // Geschwindigkeit aus dem Slider-Wert erhalten
                foreach (Flummy flummy in flummies)
                {
                    flummy.SetSpeed(speed); // Setze die Geschwindigkeit für die Flummy-Instanz
                }
            }
            Spielfeld.Background = Füllung;
        }

        private void SliderBals_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GameBals = (UInt16)SliderBals.Value;
        }

        private async void CreateBals()
        {

            UInt16 Left = 10;
            UInt16 Top = 20;
            for (UInt16 i = 0; i <= GameBals; i++)
            {
                flummyBall = new Flummy(Spielfeld, Left, Top, RandomBool(), RandomBool());
                flummies.Add(flummyBall);

                Left += (UInt16)random.Next(20, 120);
                Top += 15;
                await Task.Delay(500);
            }
        }

        public bool RandomBool()
        {
            return random.Next(2) == 0;
        }
    }

    
}
