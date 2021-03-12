using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{


    public class LineFunction
    {
        public double Slope { get; set; }

        public double VerticalTranslation { get; set; }

        public LineFunction(Point p1, Point p2)
        {
            // Opstellen functie van een rechte :
            // Y-Y1 = (Y1-Y2)/(X1-X2)*(X-X1)
            if(p1.X- p2.X == 0)
            {
                Slope = 100000000000;
                VerticalTranslation = 0;
            }
            else
            {
                Slope = (p1.Y - p2.Y) / (p1.X - p2.X);
                VerticalTranslation = p1.Y - Slope * p1.X;
            }
            
        }

        public Tuple<bool, Point> intersection(LineFunction r2)
        {
            double x;
            double y;

            if (this.Slope - r2.Slope == 0)
            {
                Point p1 = new Point(0, 0); 
                return Tuple.Create(false, p1);
            }
            else
            {
                x = (r2.VerticalTranslation - this.VerticalTranslation) / (this.Slope - r2.Slope);
                y = this.Slope * x + this.VerticalTranslation;
            }


            Point Vluchtpunt = new Point(x,y);
            return Tuple.Create(true,Vluchtpunt);
        }
    }
}

