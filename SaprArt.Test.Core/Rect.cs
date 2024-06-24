using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core
{
    public readonly record struct Rect
    {
        public Rect(Point point1, Point point2, Color color)
        {
            var (minX, maxX) = Utils.FindMinMax(point1.X, point2.X);
            var (minY, maxY) = Utils.FindMinMax(point1.Y, point2.Y);

            TopLeft = new Point(minX, minY);
            BottomRight = new Point(maxX, maxY);
            Width = maxX - minX;
            Height = maxY - minY;
            Center = new Point(minX + Width / 2, minY + Height / 2);
            Color = color;
        }

        public Point TopLeft { get; }

        public Point BottomRight { get; }

        public Point Center { get; }

        public Double Width { get; }

        public Double Height { get; }

        public Color Color { get; }
    }
}
