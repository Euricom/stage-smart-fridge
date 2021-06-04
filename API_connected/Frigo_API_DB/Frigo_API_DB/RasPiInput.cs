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
        public RasPiInput(float prob, int tag, Circumference k)
        {
            switch (tag)
            {
                case 1:
                    Tagname = "ColaCan";
                    break;
                case 2:
                    Tagname = "FantaCan";
                    break;
                case 3:
                    Tagname = "SpriteLemonCan";
                    break;
                case 4:
                    Tagname = "Upperside";
                    break;
                case 5:
                    Tagname = "VanishingLine";
                    break;
                default:
                    Tagname = "UnKnown";
                    break;
            }

            Probability = prob;
            Tagid = tag;
            Boundingbox = k;
        }
        public RasPiInput(string name, float prob, int tag, Circumference k)
        {
            Tagname = name;
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
