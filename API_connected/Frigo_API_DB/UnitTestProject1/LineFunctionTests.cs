using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frigo_API_DB;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestsFrigo
{
    [TestClass]
    public class LineFunctionTests
    {
        [TestMethod]
        public void LineFunction_Should_Calculat_The_Intersection_Of_two_Lines()
        {
            //Arrange
            LineFunction line1 = new LineFunction();
            line1.Slope = 13;
            line1.VerticalTranslation = 27;

            LineFunction line2 = new LineFunction();
            line2.Slope = 7;
            line2.VerticalTranslation = -5;

            bool good;
            Point intersection;
            Point expectedIntersection = new Point(-16.0 / 3.0, -127.0 / 3.0);

            //act
            (good, intersection) = line1.intersection(line2);

            //Assert
            Assert.IsTrue(good);
            Assert.AreEqual(expectedIntersection.X, intersection.X);
            Assert.AreEqual(expectedIntersection.Y, intersection.Y);
        }
    }
}
