using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frigo_API_DB;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Zone z = new Zone(1,2,3);
            int expected = 5;
            int actual = z.tellen(2, 3);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void inDeZone()
        {
            Zone z = new Zone(1, 2, 3);
            DrankP D = new DrankP();
            Assert.IsTrue(z.inTheZone(D));
        }


        public void centerCalculator()
        {
            bereken ber = new bereken();
            DrankP uperSideCan = new DrankP(string naam, float prob, int tag, double h, double l, double t, double w);
            ber.calculateThecenter(uperSideCan);
        }
        
    }
}
