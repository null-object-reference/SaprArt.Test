using System.Drawing;

namespace SaprArt.Test.Core.Tests
{
    [TestClass]
    public class PublicApiTests
    {

        private static readonly Rect[] _rectList = [CommonData.Rect1, CommonData.Rect2, CommonData.Rect3, CommonData.Rect4,
            CommonData.Rect5, CommonData.Rect6, CommonData.Rect7, CommonData.RectYellow];

        [TestMethod]
        public void WrapConsideringVerticesOfAllOverlappedRectangles()
        {
            var wrappingRectangle = _rectList.Wrap(wrapByRect: CommonData.SelectionRect,
                wrapPolicy: WrapPolicy.ConsiderVerticesOfAllOverlappedRectangles,
                excludeByColor: [Color.Yellow]);

            Assert.AreEqual(expected: new(new(3, -2), new(14, 11), color: default), actual: wrappingRectangle);
        }

        [TestMethod]
        public void WrapConsideringVerticesOfAllOverlappedRectangles_EmptyRectList()
        {
            var wrappingRectangle = Array.Empty<Rect>().Wrap(wrapByRect: CommonData.SelectionRect,
                wrapPolicy: WrapPolicy.ConsiderVerticesOfAllOverlappedRectangles,
                excludeByColor: [Color.Yellow]);

            Assert.AreEqual(expected: new(new(0, 0), new(0, 0), color: default), actual: wrappingRectangle);
        }

        [TestMethod]
        public void WrapConsideringOnlyVerticesLocatedInOverlappingRectangle()
        {
            var wrappingRectangle = _rectList.Wrap(wrapByRect: CommonData.SelectionRect,
                wrapPolicy: WrapPolicy.ConsiderOnlyVerticesLocatedInOverlappingRectangle,
                excludeByColor: [Color.Yellow]);

            Assert.AreEqual(expected: new(new(3, 2), new(12, 11), color: default), actual: wrappingRectangle);
        }

        [TestMethod]
        public void WrapConsideringOnlyVerticesLocatedInOverlappingRectangle_EmptyRectList()
        {
            var wrappingRectangle = Array.Empty<Rect>().Wrap(wrapByRect: CommonData.SelectionRect,
                wrapPolicy: WrapPolicy.ConsiderOnlyVerticesLocatedInOverlappingRectangle,
                excludeByColor: [Color.Yellow]);

            Assert.AreEqual(expected: new(new(0, 0), new(0, 0), color: default), actual: wrappingRectangle);
        }
    }
}
