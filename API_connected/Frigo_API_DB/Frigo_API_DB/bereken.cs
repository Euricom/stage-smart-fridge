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
        private List<DrankP> BovenkantenLijst = new List<DrankP>();
        private List<DrankP> Vluchtlijnen = new List<DrankP>();

        private int fantaAantal = 0;
        private int colaAantal = 0;
        private int spriteAantal = 0;
        private int overschot = 0;
        
        private List<Hoeveelheid> terug = new List<Hoeveelheid>();
        private Zone fantaZone;
        private Zone colaZone;
        private Zone spriteZone;
        private List<Point> centerPointsUpperside = new List<Point>();
        private Point vanishingPoint;
        
       
        public Point Counter(List<DrankP> dranken)
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
                else if (dranken[i].Tagname == "VluchtLijn" && dranken[i].Probability > 0)
                {
                    Vluchtlijnen.Add(dranken[i]);
                }
            }

            //Zone per tagnaam maken
            calculateVanishingpoint();

            /*
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
            */
            return vanishingPoint;
        }


        public void calculateVanishingpoint()
        {
            List<RechteFunctie> rechten = new List<RechteFunctie>();

            // Dit berekent de coordinaten van de de lijn. Height moet opgeteld worden omdat de Yas naar beneden staat.
            for ( int i = 0; i < Vluchtlijnen.Count(); i++)
            {
                if(Vluchtlijnen[i].Boundingbox.Left < 0.2)
                {
                    double X1 = Vluchtlijnen[i].Boundingbox.Left;
                    double Y1 = Vluchtlijnen[i].Boundingbox.Top + Vluchtlijnen[i].Boundingbox.Height;
                    Point linksOnder = new Point(X1, Y1);

                    double X2 = Vluchtlijnen[i].Boundingbox.Left + Vluchtlijnen[i].Boundingbox.Width;
                    double Y2 = Vluchtlijnen[i].Boundingbox.Top;
                    Point rechtsBoven = new Point(X2, Y2);

                    RechteFunctie r = new RechteFunctie(linksOnder, rechtsBoven);
                    rechten.Add(r);
                }

                else
                {
                    double X1 = Vluchtlijnen[i].Boundingbox.Left;
                    double Y1 = Vluchtlijnen[i].Boundingbox.Top;
                    Point linksBoven = new Point(X1, Y1);

                    double X2 = Vluchtlijnen[i].Boundingbox.Left + Vluchtlijnen[i].Boundingbox.Width;
                    double Y2 = Vluchtlijnen[i].Boundingbox.Top + Vluchtlijnen[i].Boundingbox.Height;
                    Point rechtsOnder = new Point(X2, Y2);

                    RechteFunctie r = new RechteFunctie(linksBoven, rechtsOnder);
                    rechten.Add(r);
                }
            }

            // Dan intersectie zoeken. 
            bool parallel;
            List<Point> punten = new List<Point>();
            for(int i = 0; i < rechten.Count() -1; i++)
            {
                for(int j = i+1; j < rechten.Count(); j++)
                {
                    Point punt;
                    (parallel, punt) = rechten[i].intersection(rechten[j]);
                    if (parallel)
                    {
                        punten.Add(punt);
                    }
                }
            }

            //Bij meerdere vluchtlijn gemiddelde berekenen
            double somX = 0;
            double somY = 0;
            for (int i =0; i < punten.Count(); i++)
            {
                somX += punten[i].X; 
                somY += punten[i].Y;
            }


            double x = somX / punten.Count();
            double y = somY / punten.Count();
            this.vanishingPoint = new Point(x, y);


        }


        public Zone makeTheZone(List<DrankP> drinken)
        {
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
