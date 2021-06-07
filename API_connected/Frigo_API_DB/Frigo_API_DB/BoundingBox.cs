using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class BoundingBox
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Bottom { get; set; }
        public double Right { get; set; } 

        public Circumference ConvertToCircumference()
        {
            double Height = Bottom - Top;
            double Width = Right - Left;
            Circumference CustomCircumference = new Circumference(Height, Left, Top, Width);
            return CustomCircumference;
        }
    }

}
