using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Calculating
    {
        private List<RasPiInput> fanta = new List<RasPiInput>();
        private List<RasPiInput> cola = new List<RasPiInput>();
        private List<RasPiInput> sprite = new List<RasPiInput>();
        private List<RasPiInput> upperSideList = new List<RasPiInput>();
        private List<RasPiInput> vanishingLineCircumference = new List<RasPiInput>();

        private Point vanishingPoint;

        private List<Point> centerPointsUpperside = new List<Point>();

        private List<LineFunction> colaZone = new List<LineFunction>();
        private List<LineFunction> fantaZone = new List<LineFunction>();
        private List<LineFunction> spriteZone = new List<LineFunction>();

        private int fantaAmount = 0;
        private int colaAmount = 0;
        private int spriteAmount = 0;
        
        private List<Amounts> result = new List<Amounts>();
        
        
       
        public List<Amounts> Counter(List<RasPiInput> dranken)
        {
            //Verdelen per tagnaam
            for(int i = 0; i < dranken.Count(); i++)
            {
                if(dranken[i].Tagname == "Cola-blik" && dranken[i].Probability > 0.40)
                {
                    this.cola.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "Fanta" && dranken[i].Probability > 0.40)
                {
                    this.fanta.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "Sprite-Lemon-blik" && dranken[i].Probability > 0.40)
                {
                    this.sprite.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "bovenkant" && dranken[i].Probability > 0.40)
                {
                    upperSideList.Add(dranken[i]);
                }
                else if (dranken[i].Tagname == "VluchtLijn" && dranken[i].Probability > 0)
                {
                    vanishingLineCircumference.Add(dranken[i]);
                }
            }

            //Zone per tagnaam maken
            calculateVanishingpoint();


            fantaZone = makeTheZone(this.fanta);
            colaZone = makeTheZone(this.cola);
            spriteZone = makeTheZone(this.sprite);
            
            //Middekanten van blik worden berekend.
            for (int i = 0; i < upperSideList.Count(); i++)
            {
                centerPointsUpperside.Add(calculateThecenter(upperSideList[i].Boundingbox));
            }

            fantaAmount = inTheZone(centerPointsUpperside, fantaZone);
            colaAmount = inTheZone(centerPointsUpperside, colaZone);
            spriteAmount = inTheZone(centerPointsUpperside, spriteZone);

            Amounts colaTotal = new Amounts(1, "Cola-blik", colaAmount);
            Amounts fantaTotal = new Amounts(2, "Fanta", fantaAmount);
            Amounts spriteTotal = new Amounts(3, "Sprite-Lemon-blik", spriteAmount);
            result.Add(colaTotal);
            result.Add(fantaTotal);
            result.Add(spriteTotal);
            return result;
        }


        public void calculateVanishingpoint()
        {
            List<LineFunction> vanishingLines = new List<LineFunction>();

            // Dit berekent de coordinaten van de de lijn. Height moet opgeteld worden omdat de Yas naar beneden staat.
            for ( int i = 0; i < vanishingLineCircumference.Count(); i++)
            {
                if(vanishingLineCircumference[i].Boundingbox.Left < 0.2)
                {
                    double x1 = vanishingLineCircumference[i].Boundingbox.Left;
                    double y1 = vanishingLineCircumference[i].Boundingbox.Top + vanishingLineCircumference[i].Boundingbox.Height;
                    Point bottomLeftSide = new Point(x1, y1);

                    double x2 = vanishingLineCircumference[i].Boundingbox.Left + vanishingLineCircumference[i].Boundingbox.Width;
                    double y2 = vanishingLineCircumference[i].Boundingbox.Top;
                    Point upperRichtSide = new Point(x2, y2);

                    LineFunction r = new LineFunction(bottomLeftSide, upperRichtSide);
                    vanishingLines.Add(r);
                }

                else
                {
                    double X1 = vanishingLineCircumference[i].Boundingbox.Left;
                    double Y1 = vanishingLineCircumference[i].Boundingbox.Top;
                    Point linksBoven = new Point(X1, Y1);

                    double X2 = vanishingLineCircumference[i].Boundingbox.Left + vanishingLineCircumference[i].Boundingbox.Width;
                    double Y2 = vanishingLineCircumference[i].Boundingbox.Top + vanishingLineCircumference[i].Boundingbox.Height;
                    Point rechtsOnder = new Point(X2, Y2);

                    LineFunction r = new LineFunction(linksBoven, rechtsOnder);
                    vanishingLines.Add(r);
                }
            }

            // Dan intersectie zoeken. 
            bool parallel;
            List<Point> points = new List<Point>();
            for(int i = 0; i < vanishingLines.Count() -1; i++)
            {
                for(int j = i+1; j < vanishingLines.Count(); j++)
                {
                    Point point;
                    (parallel, point) = vanishingLines[i].intersection(vanishingLines[j]);
                    if (parallel)
                    {
                        points.Add(point);
                    }
                }
            }

            //Bij meerdere vluchtlijn gemiddelde berekenen
            double sumX = 0;
            double somY = 0;
            for (int i =0; i < points.Count(); i++)
            {
                sumX += points[i].X; 
                somY += points[i].Y;
            }


            double x = sumX / points.Count();
            double y = somY / points.Count();
            this.vanishingPoint = new Point(x, y);


        }

        // Nog opvangen als ik een horizontale lijn krijg.
        public List<LineFunction> makeTheZone(List<RasPiInput> frontSide)
        {
            List<LineFunction> zone = new List<LineFunction>();

            //Meest linkse punt bepalen uit de lijst, hiervan de linker bovenhoek nemen.
            int minLeftId = 0;
            double minimum = 1;
            for(int i = 0; i < frontSide.Count(); i++)
            {
                if(frontSide[i].Boundingbox.Left < minimum)
                {
                    minimum = frontSide[i].Boundingbox.Left;
                    minLeftId = i;
                }
            }

            double x1 = frontSide[minLeftId].Boundingbox.Left;
            double y1 = frontSide[minLeftId].Boundingbox.Top;
            Point left = new Point(x1, y1);

            LineFunction r1 = new LineFunction(left, vanishingPoint);
            zone.Add(r1);


            //Zelfde voor rechts
            int maxRightId = 0;
            double maximum = 0;
            for (int i = 0; i < frontSide.Count(); i++)
            {
                if (frontSide[i].Boundingbox.Left + frontSide[i].Boundingbox.Width > maximum)
                {
                    maximum= frontSide[i].Boundingbox.Left + frontSide[i].Boundingbox.Width;
                    maxRightId= i;
                }
            }
            double x2 = frontSide[maxRightId].Boundingbox.Left + frontSide[maxRightId].Boundingbox.Width;
            double y2 = frontSide[maxRightId].Boundingbox.Top;
            Point rechts = new Point(x2, y2);

            LineFunction r2 = new LineFunction(rechts, vanishingPoint);
            zone.Add(r2);

            return zone;
        }


        public int inTheZone(List<Point> centers, List<LineFunction> zone)
        {
            // Zoek X in functie van Y en dan het punt invullen en kijken of het langs de goede kant van de lijn ligt.
            // De zone is steeds zo opgebouwd dat de eerste rechte de linker is en de 2de de rechter
            // funcite in x:   X=Y/a-b/a
            int amount = 0;
            for (int i = 0; i < centers.Count(); i++)
            {
                double xLineLeft = centers[i].Y / zone[0].Slope - zone[0].VerticalTranslation / zone[0].Slope;
                double xLijnRight = centers[i].Y / zone[1].Slope - zone[1].VerticalTranslation / zone[1].Slope;
                double x = centers[i].X;
                                
                if(xLineLeft < x && x < xLijnRight)
                {
                    amount++;
                }
            }
            
            //aanpassen
            return amount;
           
        }




        public Point calculateThecenter(Circumference circumference)
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
