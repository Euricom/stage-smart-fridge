using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.DTO
{
    public class RasPiModel
    {
        public string Tagname { get; set; }
        public double Probability { get; set; }
        public int Tagid { get; set; }
        public Circumference Boundingbox { get; set; }
    }
}
