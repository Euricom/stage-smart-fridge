using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Frigo_API_DB
{
    public class DrankP
    {
        public string tagname { get; set; }
        public float probability { get; set; }
        public int tagid { get; set; }
        public  kader boundingBox { get; set; }

        public DrankP()
        { 
            // Hier gebeurd niets, maar dit is nodig om de json data die via de post binnenkomt op te vangen.
        }

        public DrankP(string naam, float prob, int tag, kader k)
        {
            tagname = naam;
            probability = prob;
            tagid = tag;
            boundingBox = k;
        }

        public DrankP(string naam, float prob, int tag, double h, double l, double t, double w)
        {
            tagname = naam;
            probability = prob;
            tagid = tag;
            boundingBox = new kader(h, l, t, w);
        }
    }
}
