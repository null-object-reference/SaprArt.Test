using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core
{
    internal static class RectExtensions
    {
        internal static Boolean IsOverlappingBy(this Rect rect1, Rect rect2)
        {
            var hDelta = Math.Abs(rect1.Center.X - rect2.Center.X);
            var vDelta = Math.Abs(rect1.Center.Y - rect2.Center.Y);

            return (rect1.Width / 2 + rect1.Height / 2) >= hDelta
                && (rect1.Height / 2 + rect2.Height / 2) >= vDelta;
        }

        internal static Point[] GetVerticesOverlappedBy(this Rect rect1, Rect rect2)
        {
            var topRight = new Point(rect1.BottomRight.X, rect1.TopLeft.Y);
            var bottomLeft = new Point(rect1.TopLeft.X, rect1.BottomRight.Y);
            var vertices = new Point[] { rect1.TopLeft, topRight, rect1.BottomRight, bottomLeft };
            var outputList = new List<Point>(capacity: vertices.Length);
            foreach (var v in vertices)
            {
                if (v.IsBelongToRect(rect2))
                    outputList.Add(v);
            }

            return outputList.ToArray();
        }
    }
}
