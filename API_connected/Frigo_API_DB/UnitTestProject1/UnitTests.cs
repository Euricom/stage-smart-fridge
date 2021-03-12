using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frigo_API_DB;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTests
    {
        public void centerCalculator()
        {
            Calculating ber = new Calculating();
            string naam = "cola";
            double prob = 0.745;
            int tag = 1;
            double h = 0.321515185;
            double l = 0.38555485;
            double t = 0.74629482;
            double w = 0.685265152;
            RasPiInput uperSideCan = new RasPiInput(naam, prob, tag, h, l, t, w);
            ber.calculateThecenter(uperSideCan.Boundingbox);
        }

        public void vanishPointChecker()
        {
            Calculating ber = new Calculating();
            
            Point P1 = new Point(1, 1);
            Point P2 = new Point(2, 3);
        }
        
    }
}
