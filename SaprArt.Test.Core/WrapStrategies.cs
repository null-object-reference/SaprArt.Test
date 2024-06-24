using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core
{
    internal static class WrapStrategies
    {
        internal delegate Rect WrapStrategy(Rect[] rects, Rect wrapByRect);

        internal static WrapStrategy GetStrategy(WrapPolicy policy)
            => policy switch
            {
                WrapPolicy.ConsiderVerticesOfAllOverlappedRectangles
                    => StrategyConsiderVerticesOfAllOverlappedRectangles,
                WrapPolicy.ConsiderOnlyVerticesLocatedInOverlappingRectangle
                    => StrategyConsiderOnlyVerticesLocatedInOverlappingRectangle,
                _ => throw new ArgumentException($"Unknown wrap policy: {policy}"),
            };

        private static Rect StrategyConsiderVerticesOfAllOverlappedRectangles(Rect[] rects, Rect wrapByRect)
        {
            if (rects.Length == 0) return default;

            Double minX = Double.MaxValue, minY = Double.MaxValue, maxX = Double.MinValue, maxY = Double.MinValue;

            foreach (var rect in rects)
            {
                minX = Math.Min(minX, rect.TopLeft.X);
                minY = Math.Min(minY, rect.TopLeft.Y);

                maxX = Math.Max(maxX, rect.BottomRight.X);
                maxY = Math.Max(maxY, rect.BottomRight.Y);
            }

            return new(point1: new(minX, minY), point2: new(maxX, maxY), color: default);
        }

        private static Rect StrategyConsiderOnlyVerticesLocatedInOverlappingRectangle(Rect[] rects, Rect wrapByRect)
        {
            if (rects.Length == 0) return default;

            Double minX = Double.MaxValue, minY = Double.MaxValue, maxX = Double.MinValue, maxY = Double.MinValue;

            foreach (var rect in rects)
            {
                var points = rect.GetVerticesOverlappedBy(wrapByRect);
                UpdateMinMaxXY(ref minX, ref minY, ref maxX, ref maxY, points);
            }

            return new(point1: new(minX, minY), point2: new(maxX, maxY), color: default);
        }

        private static void UpdateMinMaxXY(ref Double minX, ref Double minY, ref Double maxX, ref Double maxY, params Point[] points)
        {
            foreach (var p in points)
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);

                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            }
        }
    }
}
