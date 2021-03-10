using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Bereken
    {
        private List<DrankP> Fanta = new List<DrankP>();
        private List<DrankP> Cola = new List<DrankP>();
        private List<DrankP> Sprite = new List<DrankP>();
        private int fantaAantal = 0;
        private int colaAantal = 0;
        private int spriteAantal = 0;
        private int overschot = 0;
        private List<DrankP> BovenkantenLijst = new List<DrankP>();
        private List<Hoeveelheid> terug = new List<Hoeveelheid>();
        private Zone fantaZone;
        private Zone colaZone;
        private Zone spriteZone;
        private List<Point> centerPointsUpperside = new List<Point>();
        


        public List<Hoeveelheid> Counter(List<DrankP> dranken)
        {
            //Verdelen per tagnaam
            for(int i = 0; i < dranken.Count(); i++)
            {
                if(dranken[i].Tagname == "Cola-blik" && dranken[i].Probability > 0.40)
                {
                    Cola.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "Fanta" && dranken[i].Probability > 0.40)
                {
                    Fanta.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "Sprite-Lemon-blik" && dranken[i].Probability > 0.40)
                {
                    Sprite.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "bovenkant" && dranken[i].Probability > 0.40)
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
                centerPointsUpperside.Add(calculateThecenter(BovenkantenLijst[i].Boundingbox));
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
            //Oke wat heb i nodig:
            // Mijn vluchtpunt en mijn basis (onderkanten van de voorste blikken of bovenkanten dit hangt af van waar ze staan op de foto).


            double Links = drinken.Min(drank => drank.Boundingbox.Left);

            // Voor de rechter en onderkant moet ik eerst die kanten berekenen door de breedte erbij op
            // te tellen of de hoogtre ervanaf.

            List<Double> rechterKanten = new List<double>();
            for(int i = 0; i < drinken.Count(); i++)
            {
                rechterKanten.Add(drinken[i].Boundingbox.Left + drinken[i].Boundingbox.Width);
            }
            double rechts = rechterKanten.Max();

            List<double> onderKanten = new List<double>();
            for (int i = 0; i < drinken.Count(); i++)
            {
                onderKanten.Add(drinken[i].Boundingbox.Top - drinken[i].Boundingbox.Height);
            }
            double onderKant = onderKanten.Min();


            Zone z = new Zone(Links, rechts, onderKant);
            return z;
        }



        public Point calculateThecenter(kader circumference)
        {
            double x;
            double y;
            x = circumference.Left + circumference.Width/2;
            y = circumference.Top - circumference.Height/2;
            Point center = new Point(x, y);
            return center;
        }

    }
}
