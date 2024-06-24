using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core
{
    internal static class Utils
    {
        internal static (Double min, Double max) FindMinMax(Double value1, Double value2)
        {
            if (value1 < value2) return (value1, value2);

            return (value2, value1);
        }
    }
}
