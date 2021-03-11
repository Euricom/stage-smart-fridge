using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frigo_API_DB;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void inDeZone()
        {
            Zone z = new Zone(1, 2, 3);
            DrankP D = new DrankP();
            Assert.IsTrue(z.inTheZone(D));
        }


        public void centerCalculator()
        {
            Bereken ber = new Bereken();
            string naam = "cola";
            double prob = 0.745;
            int tag = 1;
            double h = 0.321515185;
            double l = 0.38555485;
            double t = 0.74629482;
            double w = 0.685265152;
            DrankP uperSideCan = new DrankP(naam, prob, tag, h, l, t, w);
            ber.calculateThecenter(uperSideCan.Boundingbox);
        }

        public void vanishPointChecker()
        {
            Bereken ber = new Bereken();
            
            Point P1 = new Point(1, 1);
            Point P2 = new Point(2, 3);
        }
        
    }
}
