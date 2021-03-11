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
        public double Onderkant { get; set; }
        public double Links { get; set; }
        public double Rechts { get; set; }

        
        
        
        public Zone(double l, double r, double o)
        {
            Links = l;
            Rechts = r;
            Onderkant = o;
        }

     

        public bool inTheZone(Point centerPoint)
        {
           
            return true;
        }
    }
}
