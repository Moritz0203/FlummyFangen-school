using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlummyFangen
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    class Flummy
    {
        Grid ballContainer; // Grid als Container für Ellipse und TextBlock
        Grid feld;
        Ellipse ball;
        double x, y;
        double dx = 5, dy = 5;
        bool nachRechts = true;
        bool nachUnten = true;
        bool stateGame = false; // false = Verloren / true = gewonnen 
        SolidColorBrush Füllung;
        DispatcherTimer timer;
        TextBlock countdownTextBlock;

        public Flummy(Grid Spielfeld, double Left, double Top, bool Richtung, bool Oben)
        {
            nachRechts = Richtung;
            nachUnten = Oben;
            x = Left;
            y = Top;

            Füllung = new SolidColorBrush();
            Füllung.Color = Colors.Green;

            // Erstellen Sie das Grid als Container für Ellipse und TextBlock
            ballContainer = new Grid();
            ballContainer.VerticalAlignment = VerticalAlignment.Top;
            ballContainer.HorizontalAlignment = HorizontalAlignment.Left;

            // Erstellen Sie die Ellipse und fügen Sie sie dem Grid hinzu
            ball = new Ellipse();
            ball.Fill = Füllung;
            ball.Height = 50;
            ball.Width = 50;
            ballContainer.Children.Add(ball);

            // Erstellen Sie den TextBlock für den Countdown und fügen Sie ihn dem Grid hinzu
            countdownTextBlock = new TextBlock();
            countdownTextBlock.Text = "";
            countdownTextBlock.Foreground = new SolidColorBrush(Colors.White);
            countdownTextBlock.VerticalAlignment = VerticalAlignment.Center;
            countdownTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            ballContainer.Children.Add(countdownTextBlock);

            feld = Spielfeld;
            feld.Margin = new Thickness(5, 5, 5, 5);

            feld.Children.Add(ballContainer);

            ballContainer.Margin = new Thickness(x, y, 0, 0);

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            // Fügen Sie das MouseLeftButtonDown-Ereignis hinzu
            ballContainer.MouseLeftButtonDown += Ball_MouseLeftButtonDown;
        }

        public void StartTimer()
        {
            countdownTextBlock.Text = "10";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 1 Sekunde
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Timer-Tick-Handler wird alle 1 Sekunde aufgerufen
            // Aktualisieren Sie den Countdown-Text
            int remainingTime = int.Parse(countdownTextBlock.Text);
            remainingTime--;

            countdownTextBlock.Text = remainingTime.ToString();

            if (remainingTime <= 0)
            {
                // Timer abgelaufen, den Ball aus dem Spiel entfernen
                timer.Stop();
                timer = null;
                stateGame = false;
            }
        }

        public DispatcherTimer Timer 
        {
            get {
                return timer;
            }
        }

        public bool StateGame 
        {
            get {
                return stateGame;
            }
        }

        public SolidColorBrush BrushColor
        {
            set {
                ball.Fill = value;
            }
        }

        public Visibility GridVisibility
        {
            set
            {
                ballContainer.Visibility = value;
            }
        }

        public double SetSpeed
        {
            set
            {
                dx = value;
                dy = value;
            }
            get { return dx; }
        }

        public string SetTextBall
        {
            set
            {
                countdownTextBlock.Text = value;
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (nachRechts)
            {
                x += dx;
            }
            else
            {
                x -= dx;
            }

            if (nachUnten)
            {
                y += dy;
            }
            else
            {
                y -= dy;
            }

            // Überprüfe Kollisionen mit den Spielfeldgrenzen
            if (x <= 0 || x + ballContainer.ActualWidth >= feld.ActualWidth)
            {
                nachRechts = !nachRechts;
            }

            if (y <= 0 || y + ballContainer.ActualHeight >= feld.ActualHeight)
            {
                nachUnten = !nachUnten;
            }

            // Aktualisiere die Position des Grids (und somit von Ellipse und TextBlock)
            ballContainer.Margin = new Thickness(x, y, 0, 0);
        }

        private void Ball_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null; // Setzen Sie den Timer auf null, um ihn zu löschen
                stateGame = true;
            }
        }
    }

}
