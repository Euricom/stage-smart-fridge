using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frigo_API_DB;

namespace Frigo.spec
{
    public class ZoneTest
    {
        [Fact]
        public void optellen()
        {
            //Arrange
            int expected = 5;

            //act
            int actual = Zone.tellen(2, 3);

            //Assert
            Assert.Equal(expected, actual);

        }
    }
}
