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
        public List<RasPiInput> VanishingLineCircumference = new List<RasPiInput>();

        public List<LineFunction> VanishingLines = new List<LineFunction>();
        public Point VanishingPoint;

        private List<Point> centerPointsUpperside = new List<Point>();

        private List<LineFunction> colaZone = new List<LineFunction>();
        private List<LineFunction> fantaZone = new List<LineFunction>();
        private List<LineFunction> spriteZone = new List<LineFunction>();
        bool cZone;
        bool fZone;
        bool sZone;

        private int fantaTotal = 0;
        private int colaTotal = 0;
        private int spriteTotal = 0;
        
        
        
        
       
        public List<Amounts> Counter(List<RasPiInput> dranken)
        {
            List<Amounts> result = new List<Amounts>();
            //Verdelen per tagnaam
            for(int i = 0; i < dranken.Count(); i++)
            {
                if(dranken[i].Tagname == "ColaCan" && dranken[i].Probability > 0.40)
                {
                    this.cola.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "FantaCan" && dranken[i].Probability > 0.40)
                {
                    this.fanta.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "SpriteLemonCan" && dranken[i].Probability > 0.40)
                {
                    this.sprite.Add(dranken[i]);
                }

                else if (dranken[i].Tagname == "Upperside" && dranken[i].Probability > 0.40)
                {
                    upperSideList.Add(dranken[i]);
                }
                else if (dranken[i].Tagname == "Vanishingline" && dranken[i].Probability > 0)
                {
                    VanishingLineCircumference.Add(dranken[i]);
                }
            }

            if(VanishingLineCircumference.Count() < 2)
            {
                Amounts fout = new Amounts(0, "Bad1", 0);
                result.Add(fout);
                return result;
            }

            calculateVanishingLines();
            bool vP = calculateVanishingpoint();
            if (vP == false)
            {
                Amounts fout = new Amounts(0, "Bad2", 0);
                result.Add(fout);
                return result;
            }

            if (this.fanta.Count() > 0)
            {
                fantaZone = makeTheZone(this.fanta);
                fZone = true;
            }
            else
            {
                fZone = false;
            }
            if (this.cola.Count() > 0)
            {
                colaZone = makeTheZone(this.cola);
                cZone = true;
            }
            else
            {
                cZone = false;
            }
            if (this.sprite.Count() > 0)
            {
                spriteZone = makeTheZone(this.sprite);
                sZone = true;
                
            }
            else
            {
                sZone = false;
            }



            //Middekanten van blik worden berekend.
            for (int i = 0; i < upperSideList.Count(); i++)
            {
                centerPointsUpperside.Add(calculateThecenter(upperSideList[i].Boundingbox));
            }

            if (centerPointsUpperside.Count() == 0)
            {
                Amounts fout = new Amounts(0, "Bad3", 0);
                result.Add(fout);
                return result;
            }

            if(fZone)
            {
                this.fantaTotal = inTheZone(centerPointsUpperside, fantaZone);
            }
            if (cZone)
            {
                this.colaTotal = inTheZone(centerPointsUpperside, colaZone);
            }
            if (sZone)
            {
                this.spriteTotal = inTheZone(centerPointsUpperside, spriteZone);
            }
            

            Amounts colaAmount = new Amounts(1, "Cola-blik", colaTotal);
            Amounts fantaAmount = new Amounts(2, "Fanta", fantaTotal);
            Amounts spriteAmount = new Amounts(3, "Sprite-Lemon-blik", spriteTotal);
            result.Add(colaAmount);
            result.Add(fantaAmount);
            result.Add(spriteAmount);
            return result;
        }

        public void calculateVanishingLines()
        {
            // Dit berekent de coordinaten van de de lijn. Height moet opgeteld worden omdat de Yas naar beneden staat.
            for (int i = 0; i < VanishingLineCircumference.Count(); i++)
            {
                if (VanishingLineCircumference[i].Boundingbox.Left < 0.2)
                {
                    double x1 = VanishingLineCircumference[i].Boundingbox.Left;
                    double y1 = VanishingLineCircumference[i].Boundingbox.Top + VanishingLineCircumference[i].Boundingbox.Height;
                    Point bottomLeftSide = new Point(x1, y1);

                    double x2 = VanishingLineCircumference[i].Boundingbox.Left + VanishingLineCircumference[i].Boundingbox.Width;
                    double y2 = VanishingLineCircumference[i].Boundingbox.Top;
                    Point upperRichtSide = new Point(x2, y2);

                    LineFunction r = new LineFunction(bottomLeftSide, upperRichtSide);
                    VanishingLines.Add(r);
                }

                else
                {
                    double X1 = VanishingLineCircumference[i].Boundingbox.Left;
                    double Y1 = VanishingLineCircumference[i].Boundingbox.Top;
                    Point leftUpperSide = new Point(X1, Y1);

                    double X2 = VanishingLineCircumference[i].Boundingbox.Left + VanishingLineCircumference[i].Boundingbox.Width;
                    double Y2 = VanishingLineCircumference[i].Boundingbox.Top + VanishingLineCircumference[i].Boundingbox.Height;
                    Point rightBottomSide = new Point(X2, Y2);

                    LineFunction r = new LineFunction(leftUpperSide, rightBottomSide);
                    VanishingLines.Add(r);
                }
            }
        }

        public bool calculateVanishingpoint()
        {
            bool parallel;
            List<Point> points = new List<Point>();
            for(int i = 0; i < VanishingLines.Count() -1; i++)
            {
                for(int j = i+1; j < VanishingLines.Count(); j++)
                {
                    Point point;
                    (parallel, point) = VanishingLines[i].intersection(VanishingLines[j]);
                    if (parallel)
                    {
                        points.Add(point);
                    }
                }
            }
            if (points.Count() == 0)
            {
                return false;
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
            this.VanishingPoint = new Point(x, y);
            return true;
        }

        
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

            LineFunction r1 = new LineFunction(left, VanishingPoint);
            if(r1.Slope != 0)
            {
                zone.Add(r1);
            }
            


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

            LineFunction r2 = new LineFunction(rechts, VanishingPoint);
            if (r2.Slope != 0)
            {
                zone.Add(r2);
            }

            return zone;
        }


        public int inTheZone(List<Point> centers, List<LineFunction> zone)
        {
            // Zoek X in functie van Y en dan het punt invullen en kijken of het langs de goede kant van de lijn ligt.
            // De zone is steeds zo opgebouwd dat de eerste rechte de linker is en de 2de de rechter
            // funcite in x:   X=Y/a-b/a
            int Total = 0;
            for (int i = 0; i < centers.Count(); i++)
            {
                double xLineLeft = centers[i].Y / zone[0].Slope - zone[0].VerticalTranslation / zone[0].Slope;
                double xLijnRight = centers[i].Y / zone[1].Slope - zone[1].VerticalTranslation / zone[1].Slope;
                double x = centers[i].X;
                                
                if(xLineLeft < x && x < xLijnRight)
                {
                    Total++;
                }
            }
            
            //aanpassen
            return Total;
           
        }




        public Point calculateThecenter(Circumference circumference)
        {
            double x;
            double y;
            x = circumference.Left + circumference.Width/2;
            y = circumference.Top + circumference.Height/2;
            Point center = new Point(x, y);
            return center;
        }

    }
}
