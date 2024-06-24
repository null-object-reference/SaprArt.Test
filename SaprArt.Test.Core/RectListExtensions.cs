using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core
{
    public static class RectListExtensions
    {
        public static Rect Wrap(this Rect[] rects,
            Rect wrapByRect,
            WrapPolicy wrapPolicy,
            Color[]? includeByColor = null,
            Color[]? excludeByColor = null)
        {
            var filteredList = rects
                .FilterByColor(includeByColor, excludeByColor)
                .FindOverlappedRects(wrapByRect);
            return WrapStrategies.GetStrategy(wrapPolicy).Invoke(filteredList, wrapByRect);
        }

        internal static Rect[] FilterByColor(this Rect[] inputArray, Color[]? includeByColor, Color[]? excludeByColor)
        {
            var includeByColorFilterEnabled = includeByColor is { Length: > 0 };
            var excludeByColorFilterEnabled = excludeByColor is { Length: > 0 };

            if (!includeByColorFilterEnabled && !excludeByColorFilterEnabled) return inputArray;

            var output = new List<Rect>(capacity: inputArray.Length);

            foreach (var rect in inputArray)
            {
                if (excludeByColorFilterEnabled && excludeByColor!.Contains(rect.Color))
                    continue;
                if (!includeByColorFilterEnabled || includeByColor!.Contains(rect.Color))
                    output.Add(rect);
            }

            return output.ToArray();
        }

        internal static Rect[] FindOverlappedRects(this Rect[] findIn, Rect overlappedBy)
        {
            var output = new List<Rect>(capacity: findIn.Length);

            foreach (var rect in findIn)
            {
                if (rect.IsOverlappingBy(overlappedBy)) output.Add(rect);
            }

            return output.ToArray();
        }
    }
}
