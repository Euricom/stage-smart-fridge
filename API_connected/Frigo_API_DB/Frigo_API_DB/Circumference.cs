using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frigo_API_DB
{
    public class Circumference
    {
        public double Height { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }


        public Circumference()
        {
            // Hier gebeurd niets, maar dit is nodig om de json data die via de post binnenkomt op te vangen.
        }
        public Circumference(double h, double l, double t, double w)
        {
            Height = h;
            Left = l;
            Top = t;
            Width = w;
        }
    }
}
