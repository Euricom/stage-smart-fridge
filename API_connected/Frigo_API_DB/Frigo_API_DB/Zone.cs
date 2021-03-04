using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Zone
    {
        /*  Ideetje voor als ik toch geen spatie mag laten:
            camera vastmaken en dan de hoeken bepalen. Dus rechts en links ONDER
            zal steeds gekend zijn, en dan bepalen als rechts onder dit is, moet 
            rechts boven dit zijn. Wel veel denk werk.*/

        /*  Dit misschien als hoeken opslaan dan kan ik zien of het in de parallelogram zit */
        public double onderKant { get; set; }
        public double links { get; set; }
        public double rechts { get; set; }

        
        
        
        public Zone(double l, double r, double o)
        {
            links = l;
            rechts = r;
            onderKant = o;
        }

        public int tellen(int a, int b)
        {
            return a + b;
        }

        public bool inTheZone(Point centerPoint)
        {
            // case 1 simpel een rechthoek maken
            //case 2 iets moeilijker een parallelogram
            return true;
        }
    }
}
