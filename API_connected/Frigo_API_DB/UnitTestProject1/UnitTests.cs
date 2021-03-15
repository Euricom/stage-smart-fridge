using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frigo_API_DB;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void centerCalculator()
        {
            Calculating ber = new Calculating();
            double height = 2;
            double left = 1;
            double top = 1;
            double width = 2;
            Circumference uperSideCan = new Circumference(height, left, top, width);
            Point calculator = ber.calculateThecenter(uperSideCan);
            Point expected = new Point(2, 2);
            Assert.AreEqual(calculator.Y, expected.Y);
        }

        [TestMethod]
        public void makeVanishLines()
        {
            // stappen:
            // arrange alles opzetten 
            Calculating cal = new Calculating();
            double height1 = 0.3;
            double left1 = 0.1;
            double top1 = 0.1;
            double width1 = 0.2;
            RasPiInput vanishLine1 = new RasPiInput("vanish", 0.15518, 4, height1, left1, top1, width1);
            //S = -1.5
            //V = 0.55

            double height2 = 0.4;
            double left2 = 0.7;
            double top2 = 0.2;
            double width2 = 0.2;
            RasPiInput vanishLine2 = new RasPiInput("vanish", 0.715518, 4, height2, left2, top2, width2);
            //S = 2
            //V = -1.2

            cal.VanishingLineCircumference.Add(vanishLine1);
            cal.VanishingLineCircumference.Add(vanishLine2);

            //act: 
            cal.calculateVanishingLines();

            
            //assert
            
            Assert.AreEqual(cal.VanishingLines[1].Slope, 2);
            Assert.AreEqual(cal.VanishingLines[1].VerticalTranslation, -1.2);
            
        }

        [TestMethod]
        public void intersect()
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
            Point expectedIntersection = new Point(-16.0/3.0, -127.0/3.0);

            //act
            (good, intersection) = line1.intersection(line2);

            //Assert
            Assert.IsTrue(good);
            Assert.AreEqual(expectedIntersection.X, intersection.X);
            Assert.AreEqual(expectedIntersection.Y, intersection.Y);
        }

        [TestMethod]
        public void zoneMaker()
        {
            //Arrange
            List<LineFunction> zone = new List<LineFunction>();
            Calculating cal = new Calculating();
            Point p = new Point(0.5, 0.5);
            cal.VanishingPoint = p;
            List<RasPiInput> voorkanten = new List<RasPiInput>();

            double height1 = 0.3;
            double left1 = 0.1;
            double top1 = 0.1;
            double width1 = 0.2;
            RasPiInput blik1 = new RasPiInput("Cola", 0.15518, 4, height1, left1, top1, width1);
            voorkanten.Add(blik1);

            double height2 = 0.4;
            double left2 = 0.7;
            double top2 = 0.2;
            double width2 = 0.2;
            RasPiInput blik2 = new RasPiInput("Cola", 0.715518, 4, height2, left2, top2, width2);

            
            voorkanten.Add(blik2);

            //Act
            zone = cal.makeTheZone(voorkanten);

            //Assert
            //links
            Assert.AreEqual(zone[0].Slope, 1);
            Assert.AreEqual(zone[0].VerticalTranslation, 0);

            //rechts
            Assert.AreEqual(zone[1].Slope, -0.75);
            Assert.AreEqual(zone[1].VerticalTranslation, 0.875);
        }


    }
}
