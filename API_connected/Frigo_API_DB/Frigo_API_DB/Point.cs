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

        public Point(double xCoordinate, double yCoordinate)
        {
            X = Math.Round(xCoordinate,10);
            Y = Math.Round(yCoordinate,10);
        }
    }
}
