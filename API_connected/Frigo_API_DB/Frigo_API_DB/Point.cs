using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double xCoordinaat, double ycoordinaat)
        {
            X = xCoordinaat;
            Y = ycoordinaat;
        }
    }
}
