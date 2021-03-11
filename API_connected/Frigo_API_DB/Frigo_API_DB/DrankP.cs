using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Frigo_API_DB
{
    public class DrankP
    {
        public string Tagname { get; set; }
        public double Probability { get; set; }
        public int Tagid { get; set; }
        public  kader Boundingbox { get; set; }

        public DrankP()
        { 
            // Hier gebeurd niets, maar dit is nodig om de json data die via de post binnenkomt op te vangen.
        }

        public DrankP(string naam, float prob, int tag, kader k)
        {
            Tagname = naam;
            Probability = prob;
            Tagid = tag;
            Boundingbox = k;
        }

        public DrankP(string naam, double prob, int tag, double h, double l, double t, double w)
        {
            Tagname = naam;
            Probability = prob;
            Tagid = tag;
            Boundingbox = new kader(h, l, t, w);
        }
    }
}
