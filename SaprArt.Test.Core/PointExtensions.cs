using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core
{
    internal static class PointExtensions
    {
        internal static Boolean IsBelongToRect(this Point point, Rect rect)
        {
            return point.X >= rect.TopLeft.X && point.X <= rect.BottomRight.X
                && point.Y >= rect.TopLeft.Y && point.Y <= rect.BottomRight.Y;
        }
    }
}
