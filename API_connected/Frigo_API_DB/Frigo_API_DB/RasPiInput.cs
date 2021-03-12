using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Frigo_API_DB
{
    public class RasPiInput
    {
        public string Tagname { get; set; }
        public double Probability { get; set; }
        public int Tagid { get; set; }
        public  Circumference Boundingbox { get; set; }

        public RasPiInput()
        { 
            // Hier gebeurd niets, maar dit is nodig om de json data die via de post binnenkomt op te vangen.
        }

        public RasPiInput(string naam, float prob, int tag, Circumference k)
        {
            Tagname = naam;
            Probability = prob;
            Tagid = tag;
            Boundingbox = k;
        }

        public RasPiInput(string naam, double prob, int tag, double h, double l, double t, double w)
        {
            Tagname = naam;
            Probability = prob;
            Tagid = tag;
            Boundingbox = new Circumference(h, l, t, w);
        }
    }
}
