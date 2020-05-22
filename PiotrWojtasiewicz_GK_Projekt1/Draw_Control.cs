using System;
using System.ComponentModel;
using System.Windows.Forms;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Color = SFML.Graphics.Color;
using System.Collections.Generic;

namespace PiotrWojtasiewicz_GK_Projekt1
{
    public partial class Draw_Control : UserControl
    {
        RenderWindow renderWindow;

        public bool MouseButtonPressed = false;
        public Vector2f MousePressPosition = new Vector2f();

        public List<Drawable> shapes;
        public bool ProcessIsBusy = false;
        Line line;
        public int algorithm = 1;

        int k = 350;
        public Draw_Control()
        {
            InitializeComponent();
            shapes = new List<Drawable>()
            {
                // algorytm Bresenhama

                new Line(new Vector2f(30,50), new Vector2f(30,200), 1),
                new Line(new Vector2f(30,200), new Vector2f(60,150), 1),
                new Line(new Vector2f(60,150), new Vector2f(90,200), 1),
                new Line(new Vector2f(90,50), new Vector2f(90,200), 1),

                new Line(new Vector2f(110,50), new Vector2f(110,200), 1),

                new Line(new Vector2f(120,50), new Vector2f(180,50), 1),
                new Line(new Vector2f(150,50), new Vector2f(150,200), 1),

                new Line(new Vector2f(180,200), new Vector2f(210,50), 1),
                new Line(new Vector2f(210,50), new Vector2f(240,200), 1),
                new Line(new Vector2f(190,150), new Vector2f(230,150), 1),

                new Line(new Vector2f(250,200), new Vector2f(250,50), 1),
                new Line(new Vector2f(250,50), new Vector2f(280,100), 1),
                new Line(new Vector2f(280,100), new Vector2f(310,50), 1),
                new Line(new Vector2f(310,50), new Vector2f(310,200), 1),

                //algorytm Wu

                new Line(new Vector2f(30 + k,50), new Vector2f(30 + k,200), 2),
                new Line(new Vector2f(30 + k,200), new Vector2f(60 + k,150), 2),
                new Line(new Vector2f(60 + k,150), new Vector2f(90 + k,200), 2),
                new Line(new Vector2f(90 + k,50), new Vector2f(90 + k,200), 2),

                new Line(new Vector2f(110 + k,50), new Vector2f(110 + k,200), 2),

                new Line(new Vector2f(120 + k,50), new Vector2f(180 + k,50), 2),
                new Line(new Vector2f(150 + k,50), new Vector2f(150 + k,200), 2),

                new Line(new Vector2f(180 + k,200), new Vector2f(210 + k,50), 2),
                new Line(new Vector2f(210 + k,50), new Vector2f(240 + k,200), 2),
                new Line(new Vector2f(190 + k,150), new Vector2f(230 + k,150), 2),

                new Line(new Vector2f(250 + k,200), new Vector2f(250 + k,50), 2),
                new Line(new Vector2f(250 + k,50), new Vector2f(280 + k,100), 2),
                new Line(new Vector2f(280 + k,100), new Vector2f(310 + k,50), 2),
                new Line(new Vector2f(310 + k,50), new Vector2f(310 + k,200), 2),

            };
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            renderWindow = new RenderWindow((IntPtr)e.Argument);

            renderWindow.MouseButtonPressed += renderWindow_MouseButtonPressed;
            renderWindow.MouseButtonReleased += renderWindow_MouseButtonReleased;
            renderWindow.Resized += renderWindow_Resized;

            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();
                renderWindow.Clear(Color.White);

                Process();

                renderWindow.Display();
            }
        }

        public void StartSFML()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync(this.Handle);
            }
        }
        private void Process()
        {
            if (shapes.Count > 0)
            {
                if (!ProcessIsBusy)
                {
                    ProcessIsBusy = true;
                    foreach (Drawable item in shapes)
                    {
                        renderWindow.Draw(item);
                    }
                    ProcessIsBusy = false;
                }
            }
            if (MouseButtonPressed)
            {
                line = new Line(MousePressPosition, (Vector2f)Mouse.GetPosition(renderWindow), algorithm);
                renderWindow.Draw(line);
            }
        }
        private void renderWindow_Resized(object sender, SizeEventArgs e)
        {
            FloatRect visibleArea = new FloatRect(0, 0, e.Width, e.Height);
            renderWindow.SetView(new SFML.Graphics.View(visibleArea));
        }

        private void renderWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            MouseButtonPressed = true;
            MousePressPosition = new Vector2f(e.X, e.Y);
        }

        private void renderWindow_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            MouseButtonPressed = false;
            if (line != null)
            {
                shapes.Add(line);
                line = null;
            }
        }

        public string location = "Błąd";

        private void Draw_Control_MouseMove(object sender, MouseEventArgs e)
        {
            location = "X: " + e.X + ", Y: " + e.Y;
        }
    }
}
