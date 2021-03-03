using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class bereken
    {
        private List<DrankP> Fanta = new List<DrankP>();
        private List<DrankP> Cola = new List<DrankP>();
        private List<DrankP> Sprite = new List<DrankP>();
        private int fantaAantal = 0;
        private int colaAantal = 0;
        private int spriteAantal = 0;
        private int overschot = 0;
        private List<DrankP> BovenkantenLijst = new List<DrankP>();
        private List<hoeveelheid> terug = new List<hoeveelheid>();
        private Zone fantaZone;
        private Zone colaZone;
        private Zone spriteZone;
        private List<Point> centerPointsUpperside = new List<Point>();
        


        public List<hoeveelheid> Counter(List<DrankP> dranken)
        {
            //Verdelen per tagnaam
            for(int i = 0; i < dranken.Count(); i++)
            {
                if(dranken[i].tagname == "Cola-blik" && dranken[i].probability > 0.40)
                {
                    Cola.Add(dranken[i]);
                }

                else if (dranken[i].tagname == "Fanta" && dranken[i].probability > 0.40)
                {
                    Fanta.Add(dranken[i]);
                }

                else if (dranken[i].tagname == "Sprite-Lemon-blik" && dranken[i].probability > 0.40)
                {
                    Sprite.Add(dranken[i]);
                }

                else if (dranken[i].tagname == "bovenkant" && dranken[i].probability > 0.40)
                {
                    BovenkantenLijst.Add(dranken[i]);
                }
            }

            //Zone per tagnaam maken
            fantaZone = makeTheZone(Fanta);
            colaZone = makeTheZone(Cola);
            spriteZone = makeTheZone(Sprite);

            //Middekanten van blik worden berekend.
            for (int i = 0; i < BovenkantenLijst.Count(); i++)
            {
                centerPointsUpperside.Add(calculateThecenter(BovenkantenLijst[i].boundingBox));
            }
            


            //aanpassen
            for (int i = 0; i < centerPointsUpperside.Count(); i++ )
            {
                if (fantaZone.inTheZone(centerPointsUpperside[i]))
                {
                    fantaAantal++;
                }
                else if (colaZone.inTheZone(centerPointsUpperside[i]))
                {
                    colaAantal++;
                }
                else if (spriteZone.inTheZone(centerPointsUpperside[i]))
                {
                    spriteAantal++;
                }
                else
                {
                    overschot++;
                }
            }

            return terug;
        }


        public Zone makeTheZone(List<DrankP> drinken)
        {
            double Links = drinken.Min(drank => drank.boundingBox.left);

            // Voor de rechter en onderkant moet ik eerst die kanten berekenen door de breedte erbij op
            // te tellen of de hoogtre ervanaf.

            List<Double> rechterKanten = new List<double>();
            for(int i = 0; i < drinken.Count(); i++)
            {
                rechterKanten.Add(drinken[i].boundingBox.left + drinken[i].boundingBox.width);
            }
            double rechts = rechterKanten.Max();

            List<double> onderKanten = new List<double>();
            for (int i = 0; i < drinken.Count(); i++)
            {
                onderKanten.Add(drinken[i].boundingBox.top - drinken[i].boundingBox.height);
            }
            double onderKant = onderKanten.Min();


            Zone z = new Zone(Links, rechts, onderKant);
            return z;
        }



        public Point calculateThecenter(kader circumference)
        {
            double x;
            double y;
            x = circumference.left + circumference.width/2;
            y = circumference.top - circumference.height/2;
            Point center = new Point(x, y);
            return center;
        }

    }
}
