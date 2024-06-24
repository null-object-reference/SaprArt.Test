using SaprArt.Test.Core;
using System.Diagnostics.CodeAnalysis;

namespace SaprArt.Test.Cli
{
    internal static class Utils
    {
        internal static Boolean TryParseRect(String source,
            [NotNullWhen(true)] out Rect? rect,
            [NotNullWhen(false)] out String? error)
        {
            try
            {
                var parts = source.Split(';');
                if (parts.Length < 4)
                    throw new Exception("Incorrect format. Rect string should contain at least 4 parts");
                var x = Double.Parse(parts[0]);
                var y = Double.Parse(parts[1]);
                var width = Double.Parse(parts[2]);
                var height = Double.Parse(parts[3]);
                var color = parts.Length > 4 ? ParseColor(parts[4]) : System.Drawing.Color.Empty;

                var point1 = new Point(x, y);
                var point2 = new Point(x + width, y + height);

                rect = new(point1, point2, color);
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                rect = null;
                error = "Incorrect format. Please provide rectangle string in format \"x;y;width;height;Color?\"";
                return false;
            }
        }

        internal static Boolean TryParseRects(String source,
            [NotNullWhen(true)] out Rect[]? rects,
            [NotNullWhen(false)] out String? error)
        {
            try
            {
                var parts = source.Split(' ');
                var output = new Rect[parts.Length];
                for (var i = 0; i < parts.Length; i++)
                {
                    if (TryParseRect(parts[i].Trim(), out var parsedRect, out var message))
                        output[i] = parsedRect.Value;
                    else
                        throw new Exception(message);
                }

                rects = output;
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                rects = null;
                error = "Incorrect format. Please provide rectangle list string in format \"x1;y1;width1;height1;Color1? xN;yN;widthN;heightN;ColorN?\"";
                return false;
            }
        }

        internal static Boolean TryParseColors(String source,
            [NotNullWhen(true)] out System.Drawing.Color[]? colors,
            [NotNullWhen(false)] out String? error)
        {
            try
            {
                var parts = source.Split(' ');
                var output = new System.Drawing.Color[parts.Length];
                for (var i = 0; i < parts.Length; i++)
                {
                    var colorName = parts[i].Trim();
                    output[i] = ParseColor(colorName);
                }

                colors = output;
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                colors = null;
                error = "Incorrect Format. Please provide color list string in format \"ColorName1 ColorName2 ColorNameN\"";
                return false;
            }
        }

        private static System.Drawing.Color ParseColor(String source)
        {
            return source == "Empty"
                        ? System.Drawing.Color.Empty
                        : System.Drawing.Color.FromKnownColor(Enum.Parse<System.Drawing.KnownColor>(source));
        }
    }
}
