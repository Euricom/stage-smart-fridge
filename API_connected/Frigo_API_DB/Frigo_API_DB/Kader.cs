using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frigo_API_DB
{
    public class kader
    {
        public double height { get; set; }
        public double left { get; set; }
        public double top { get; set; }
        public double width { get; set; }


        public kader()
        {
            // Hier gebeurd niets, maar dit is nodig om de json data die via de post binnenkomt op te vangen.
        }
        public kader(double h, double l, double t, double w)
        {
            height = h;
            left = l;
            top = t;
            width = w;
        }
    }
}
