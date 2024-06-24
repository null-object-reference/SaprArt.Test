using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core.Tests
{
    internal static class CommonData
    {
        internal static readonly Rect SelectionRect = new(new(1, 1), new(13, 14), color: default);

        internal static readonly Rect Rect1 = new(new(3, 0), new(6, 5), Color.Green);
        internal static readonly Rect Rect2 = new(new(4, -2), new(10, 3), Color.Green);
        internal static readonly Rect Rect3 = new(new(8, 2), new(14, 7), Color.Green);
        internal static readonly Rect Rect4 = new(new(4, 4), new(7, 8), Color.Green);
        internal static readonly Rect Rect5 = new(new(6, 6), new(12, 11), Color.Green);
        internal static readonly Rect Rect6 = new(new(-10, -10), new(0, 0), Color.Green);
        internal static readonly Rect Rect7 = new(new(50, 50), new(60, 60), Color.Green);
        internal static readonly Rect RectYellow
            = new(new(9, 1), new(17, 12), Color.Yellow);
        internal static readonly Rect RectBlue = new(new(0, 0), new(50, 50), Color.Blue);
        internal static readonly Rect RectBlack = new(new(10, 10), new(50, 50), Color.Black);
        internal static readonly Rect RectPurple = new(new(10, 10), new(40, 40), Color.Purple);
    }
}
