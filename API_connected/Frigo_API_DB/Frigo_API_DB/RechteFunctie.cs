using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{


    public class RechteFunctie
    {
        public double Rico { get; set; }

        public double VerticalTranslation { get; set; }

        public RechteFunctie(Point P1, Point P2)
        {
            // Opstellen functie van een rechte :
            // Y-Y1 = (Y1-Y2)/(X1-X2)*(X-X1)
            if(P1.X- P2.X == 0)
            {
                Rico = 100000000000;
                VerticalTranslation = 0;
            }
            else
            {
                Rico = (P1.Y - P2.Y) / (P1.X - P2.X);
                VerticalTranslation = P1.Y - Rico * P1.X;
            }
            
        }

        public Tuple<bool, Point> intersection(RechteFunctie R2)
        {
            double X;
            double Y;

            if (this.Rico - R2.Rico == 0)
            {
                Point P1 = new Point(0, 0); 
                return Tuple.Create(false, P1);
            }
            else
            {
                X = (R2.VerticalTranslation - this.VerticalTranslation) / (this.Rico - R2.Rico);
                Y = this.Rico * X + this.VerticalTranslation;
            }


            Point Vluchtpunt = new Point(X,Y);
            return Tuple.Create(true,Vluchtpunt);
        }
    }
}

