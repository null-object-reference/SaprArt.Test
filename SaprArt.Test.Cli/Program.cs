using SaprArt.Test.Core;
using System.CommandLine;
using System.Drawing;
using System.Text;

namespace SaprArt.Test.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var selectionRectOption = new Option<Rect>(["-s", "--selection"],
                description: "Selection rectangle. String in format \"x;y;width;height;Color\". Color may be represented by one of the known color names (see https://learn.microsoft.com/en-us/dotnet/api/system.drawing.color.fromknowncolor?view=net-8.0) or by \"Empty\" as special case. e.g.: \"4;-2;6;5\" or \"4;-2;6;5;Yellow\"",
                isDefault: false,
                parseArgument: result =>
                {
                    if (Utils.TryParseRect(result.Tokens[0].Value, out var rect, out var error))
                        return rect.Value;
                    else
                    {
                        result.ErrorMessage = error;
                        return new();
                    }
                })
            {
                IsRequired = true,
            };
            var rectListOption = new Option<Rect[]>(["-r", "-rects"],
                description: "List of rectangles. String in format \"x1;y1;width1;height1;Color1 xN;yN;widthN;heightN;ColorN\". e.g.: \"3;0;3;5;Green 4;-2;6;5;Green\"",
                isDefault: false,
                parseArgument: result =>
                {
                    if (Utils.TryParseRects(result.Tokens[0].Value, out var rects, out var error))
                    {
                        return rects;
                    }
                    else
                    {
                        result.ErrorMessage = error;
                        return [];
                    }
                })
            {
                IsRequired = true,
            };
            var wrapPolicyOption = new Option<Int32>(["-p", "--wrap-policy"],
                description: "Selection (wrapping) policy. 0 - Select considering vertices of all rectangles overlapped by the selection rect. 1 - Select considering only vertices located in the selection rect")
            {
                IsRequired = true,
            };
            var includeColorsOption = new Option<Color[]?>(["--ic", "--include-colors"],
                description: "Select considering only rects with specified colors. String in format \"Color1 Color2 ColorN\"",
                isDefault: false,
                parseArgument: result =>
                {
                    if (Utils.TryParseColors(result.Tokens[0].Value, out var colors, out var error))
                        return colors;
                    else
                    {
                        result.ErrorMessage = error;
                        return [];
                    }
                });
            var excludeColorsOption = new Option<Color[]?>(["--ec", "--excludeColors"],
                description: "When selecting, ignore rects with specified colors. If both include and exclude lists have the same color, exclude list wins. String in format \"Color1 Color2 ColorN\"",
                isDefault: false,
                parseArgument: result =>
                {
                    if (Utils.TryParseColors(result.Tokens[0].Value, out var colors, out var error))
                        return colors;
                    else
                    {
                        result.ErrorMessage = error;
                        return [];
                    }
                });
            var fileOutputOption = new Option<FileInfo?>(["-f", "--file"], description: "Write results to specified path");

            var rootCommand = new RootCommand("SAPR-ART test task")
            {
                selectionRectOption,
                rectListOption,
                wrapPolicyOption,
                includeColorsOption,
                excludeColorsOption,
                fileOutputOption,
            };
            rootCommand.SetHandler((selectionRect, rectList, wrapPolicy, includeColors, excludeColors, fileOutput) =>
            {
                var resultRect = rectList.Wrap(selectionRect, (WrapPolicy)wrapPolicy, includeColors, excludeColors);

                var outputLog = new StringBuilder()
                    .AppendLine()
                    .AppendLine("===========================================================================")
                    .WriteRects("SELECTION RECT:", selectionRect)
                    .AppendLine()
                    .WriteRects("RECT LIST:", rectList)
                    .AppendLine()
                    .WriteColors("INCLUDE RECTS WITH COLORS:", includeColors)
                    .AppendLine()
                    .WriteColors("EXCLUDE RECTS WITH COLORS:", excludeColors)
                    .AppendLine()
                    .AppendLine()
                    .WriteRects("RESULT RECT:", resultRect)
                    .AppendLine("===========================================================================");

                Console.WriteLine(outputLog);

                if (fileOutput != null)
                {
                    using var outputStream = fileOutput.Open(FileMode.Append, FileAccess.Write);
                    var bytes = Encoding.UTF8.GetBytes(outputLog.ToString());
                    outputStream.Write(bytes);
                }

            }, selectionRectOption, rectListOption, wrapPolicyOption, includeColorsOption, excludeColorsOption, fileOutputOption);

            rootCommand.InvokeAsync(args);
        }
    }
}
