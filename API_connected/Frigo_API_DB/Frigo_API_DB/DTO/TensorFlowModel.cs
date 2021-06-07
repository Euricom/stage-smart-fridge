using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.DTO
{
    //Normaly I won't need the class because I Convert it in my Raspi
    public class TensorFlowModel
    {
        public List<BoundingBox> Place { get; set; }
        public List<int> Id { get; set; }
        public List<float> Probability { get; set; }
    }
}
