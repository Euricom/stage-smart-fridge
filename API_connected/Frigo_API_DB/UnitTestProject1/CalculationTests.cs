using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frigo_API_DB;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestFrigo
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void Calculator_Should_Calculate_The_Center_Of_Circumference()
        {
            Calculating ber = new Calculating();
            double height = 0;
            double left = 1;
            double top = 4;
            double width = 0;
            Circumference uperSideCan = new Circumference(height, left, top, width);
            Point calculator = ber.calculateThecenter(uperSideCan);
            Point expected = new Point(1, 4);
            Assert.AreEqual(calculator.Y, expected.Y);
        }

        [TestMethod]
        public void Calculator_Should_Calculate_VanishingLines_From_Circomferences()
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
        public void Calculator_Should_Make_The_Zone_From_VanishingPoint_LiftSide_And_RightSide()
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

        [TestMethod]
        public void Calculator_Should_Calculate_If_A_Point_Is_In_Or_Out_A_Zone()
        {
            //Arrange
            Calculating cal = new Calculating();
            List<LineFunction> zone = new List<LineFunction>();
            Point vanishingPoint = new Point(5, 7);
            Point pointLine1 = new Point(4, 3);
            LineFunction L1 = new LineFunction(pointLine1,vanishingPoint);
            Point pointLine2 = new Point(8, 2);
            LineFunction L2 = new LineFunction(pointLine2, vanishingPoint);

            zone.Add(L1);
            zone.Add(L2);
            List<Point> centers = new List<Point>();
            Point p = new Point(100, 3);
            centers.Add(p);

            //Act
            int inside = cal.inTheZone(centers, zone);

            //Assert
            Assert.AreEqual(inside, 1);


        }
    }
}
