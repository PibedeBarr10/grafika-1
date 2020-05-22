using SFML.Graphics;
using SFML.System;
using System;

namespace PiotrWojtasiewicz_GK_Projekt1
{
    class Line : Drawable
    {
        public Vector2f P1 { get; set; }
        public Vector2f P2 { get; set; }
        public int algorithm;
        private Color Color = Color.Black;
        public Line(Vector2f P1, Vector2f P2, int algorithm)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.algorithm = algorithm;
        }

        private void SetPixel(RenderTarget target, int x, int y, Color color)
        {
            target.Draw(new Vertex[] { new Vertex(new Vector2f(x, y), color) }, PrimitiveType.Points);
        }

        static private float rfpart(float x)
        {
            return 1 - fpart(x);
        }

        static private int ConvertInt(double x) { return (int)x; }
        static private int Round(double x) { return ConvertInt(x + 0.5); }

        static private float fpart(float x)
        {
            if (x < 0) return 1 - (float)(x - Math.Floor(x));
            return (float)(x - Math.Floor(x));
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            if (algorithm == 1) //algorytm Bresenhama (z punktem środkowym)
            {
                int x0 = Convert.ToInt32(P1.X);
                int y0 = Convert.ToInt32(P1.Y);

                int x1 = Convert.ToInt32(P2.X);
                int y1 = Convert.ToInt32(P2.Y);

                int dx = Math.Abs(x1 - x0);
                int dy = Math.Abs(y1 - y0);

                int x = x0;
                int y = y0;

                int sx = 0;
                if (x0 > x1)
                {
                    sx = -1;
                }
                else
                {
                    sx = 1;
                }

                int sy = 0;
                if (y0 > y1)
                {
                    sy = -1;
                }
                else
                {
                    sy = 1;
                }

                if (dx > dy)
                {
                    double err = dx / 2;
                    while (x != x1)
                    {
                        SetPixel(target, x, y, Color);
                        err -= dy;
                        if (err < 0)
                        {
                            y += sy;
                            err += dx;
                        }
                        x += sx;
                    }
                }
                else
                {
                    double err = dy / 2;
                    while (y != y1)
                    {
                        SetPixel(target, x, y, Color);
                        err -= dx;
                        if (err < 0)
                        {
                            x += sx;
                            err += dy;
                        }
                        y += sy;
                    }
                }
            }
            else if (algorithm == 2) //algorytm Wu
            {
                float x0 = P1.X;
                float y0 = P1.Y;
                float x1 = P2.X;
                float y1 = P2.Y;

                float dx = Math.Abs(x1 - x0);
                float dy = Math.Abs(y1 - y0);
                bool value = false;

                if (dy > dx)
                {
                    float tmp = x0;
                    x0 = y0;
                    y0 = tmp;

                    tmp = x1;
                    x1 = y1;
                    y1 = tmp;
                    value = true;
                }
                if (x0 > x1)
                {
                    float tmp = x0;
                    x0 = x1;
                    x1 = tmp;

                    tmp = y0;
                    y0 = y1;
                    y1 = tmp;
                }

                dx = x1 - x0;
                dy = y1 - y0;
                float gradient = dx == 0 ? 1 : dy / dx;

                int xpxl1 = Convert.ToInt32(x0);
                int xpxl2 = Convert.ToInt32(x1);
                float intersectY = y0;

                if (value)
                {
                    for (int x = xpxl1; x <= xpxl2; x++)
                    {
                        Color.A = (byte)(rfpart(intersectY) * 255);
                        SetPixel(target, ConvertInt(intersectY), x, Color);
                        Color.A = (byte)(fpart(intersectY) * 255);
                        SetPixel(target, ConvertInt(intersectY) + 1, x, Color);
                        intersectY += gradient;
                    }
                }
                else
                {
                    for (int x = xpxl1; x <= xpxl2; x++)
                    {
                        Color.A = (byte)(rfpart(intersectY) * 255);
                        SetPixel(target, x, ConvertInt(intersectY), Color);
                        Color.A = (byte)(fpart(intersectY) * 255);
                        SetPixel(target, x, ConvertInt(intersectY) + 1, Color);
                        intersectY += gradient;
                    }
                }
            }
        }
    }
}
