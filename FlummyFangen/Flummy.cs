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
    class Flummy
    {
        Ellipse ball;
        Grid feld;
        double x, y;
        double dx = 5, dy = 5;
        bool nachRechts = true;
        bool nachUnten = true;
        SolidColorBrush Füllung;

        public Flummy(Grid Spielfeld, double Left, double Top, bool Richtung, bool Oben)
        {
            nachRechts = Richtung;
            nachUnten = Oben;
            x = Left;
            y = Top;

            Füllung = new SolidColorBrush();
            Füllung.Color = Colors.Green;
            ball = new Ellipse();

            ball.Fill = Füllung;
            ball.VerticalAlignment = VerticalAlignment.Top;
            ball.HorizontalAlignment = HorizontalAlignment.Left;

            feld = Spielfeld;
            feld.Margin = new Thickness(5, 5, 5, 5);

            feld.Children.Add(ball);

            ball.Height = 50;
            ball.Width = 50;
            ball.Margin = new Thickness(x, y, 0, 0);

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            // Fügen Sie das MouseLeftButtonDown-Ereignis hinzu
            ball.MouseLeftButtonDown += Ball_MouseLeftButtonDown;
        }

        public void SetSpeed(double speed)
        {
            // Setze die Geschwindigkeit basierend auf dem übergebenen Wert
            dx = speed;
            dy = speed;
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
            if (x <= 0 || x + ball.Width >= feld.ActualWidth)
            {
                nachRechts = !nachRechts;
            }

            if (y <= 0 || y + ball.Height >= feld.ActualHeight)
            {
                nachUnten = !nachUnten;
            }

            // Aktualisiere die Position des Flummis
            ball.Margin = new Thickness(x, y, 0, 0);
        }

        private void Ball_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Ändere die Farbe auf Rot
            Füllung.Color = Colors.Red;

            dx += 1;
            dy += 1;

            // Starten Sie einen Timer, um die Farbe zurückzusetzen und die Geschwindigkeit zu reduzieren
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000); // 1 Sekunde
            timer.Tick += (s, args) =>
            {
                Füllung.Color = Colors.Green;
                timer.Stop();
            };

            timer.Start();
        }
    }
}
