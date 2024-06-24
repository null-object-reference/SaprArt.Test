using SaprArt.Test.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Cli
{
    internal static class StringBuilderExtensions
    {
        internal static StringBuilder WriteRects(this StringBuilder sb, String title, params Rect[]? rects)
        {
            var pointToString = (Point p) => $"{p.X:F1} {p.Y:F1}";
            var formatter = (StringBuilder sb, String topLeft, String bottomRight, String color) =>
            {
                sb.AppendFormat("|{0,20}|{1,20}|{2,30}|", topLeft, bottomRight, color).AppendLine();
            };

            sb.AppendLine(title);
            formatter(sb, "Top Left Corner", "Right Bottom Corner", "Color (Name/A/R/G/B)");
            if (rects != null)
                foreach (var rect in rects)
                {
                    formatter(sb, pointToString(rect.TopLeft), pointToString(rect.BottomRight), ColorToString(rect.Color));
                }

            return sb;
        }

        internal static StringBuilder WriteColors(this StringBuilder sb, String title, params System.Drawing.Color[]? colors)
        {
            var formatter = (StringBuilder sb, String name, String argb) =>
            {
                sb.AppendFormat("|{0,10}|{1,20}|", name, argb).AppendLine();
            };

            sb.AppendLine(title);
            formatter(sb, "Name", "A/R/G/B");
            if (colors != null)
                foreach (var color in colors)
                {
                    formatter(sb, color.Name, GetColorArgbString(color));
                }

            return sb;
        }

        private static String ColorToString(System.Drawing.Color color) => $"{color.Name}/{GetColorArgbString(color)}";

        private static String GetColorArgbString(System.Drawing.Color color)
            => $"{color.A:D3}/{color.R:D3}/{color.G:D3}/{color.B:D3}";
    }
}
