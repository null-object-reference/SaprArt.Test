using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaprArt.Test.Core.Tests.InternalApiTests
{
    [TestClass]
    public class RectListExtensionsTests
    {
        private static readonly Rect[] ListToFilter
            = [CommonData.RectBlue, CommonData.RectPurple, CommonData.RectBlack, CommonData.RectYellow];

        [TestMethod]
        public void FilterByColor_EmptyFilters()
        {
            var filteredList = ListToFilter.FilterByColor(includeByColor: [], excludeByColor: null);

            Assert.ReferenceEquals(filteredList, ListToFilter);
        }

        [TestMethod]
        public void FilterByColor_IncludeExcludeFilters()
        {
            var filteredList = ListToFilter.FilterByColor(
                includeByColor: [Color.Blue, Color.Purple, Color.Black],
                excludeByColor: [Color.Black]);

            Assert.IsTrue(filteredList.Length == 2);
            Assert.IsFalse(filteredList.Contains(CommonData.RectYellow));
            Assert.IsFalse(filteredList.Contains(CommonData.RectBlack));
        }

        [TestMethod]
        public void FilterByColor_IncludeByColor()
        {
            var filteredList = ListToFilter.FilterByColor(includeByColor: [Color.Yellow, Color.Black], excludeByColor: null);

            Assert.AreEqual(expected: 2, actual: filteredList.Length);
            Assert.IsTrue(filteredList.Contains(CommonData.RectYellow));
            Assert.IsTrue(filteredList.Contains(CommonData.RectBlack));
        }

        [TestMethod]
        public void FilterByColor_ExcludeByColor()
        {
            var filteredList = ListToFilter.FilterByColor(includeByColor: null, excludeByColor: [Color.Yellow]);

            Assert.AreEqual(expected: 3, actual: filteredList.Length);
            Assert.IsFalse(filteredList.Contains(CommonData.RectYellow));
        }

        [TestMethod]
        public void FindOverlappedRects()
        {
            var rectList = new Rect[] { CommonData.Rect1, CommonData.Rect2, CommonData.Rect3, CommonData.Rect4, CommonData.Rect5,
                CommonData.Rect6, CommonData.Rect7};
            var resultList = rectList.FindOverlappedRects(CommonData.SelectionRect);

            Assert.AreEqual(expected: 5, actual: resultList.Length);
            Assert.IsFalse(resultList.Contains(CommonData.Rect6));
            Assert.IsFalse(resultList.Contains(CommonData.Rect7));
        }
    }
}
