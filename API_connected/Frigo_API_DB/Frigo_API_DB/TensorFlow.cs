using Frigo_API_DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    //Normaly I won't need the class because I Convert it in my Raspi
    public class TensorFlow
    {
        public List<BoundingBox> Place { get; set; }
        public List<int> Id { get; set; }
        public List<float> Probability { get; set; }

        public TensorFlow(TensorFlowModel Model)
        {
            Place = Model.Place;
            Id = Model.Id;
            Probability = Model.Probability;
        }
        public RasPiInput ConvertToCustomModel()
        {
            RasPiInput CustomVisionValues = new RasPiInput();
            //Circumference box = Place.ConvertToCircumference();
            //RasPiInput CustomVisionValues = new RasPiInput(Probability, Id, box);
            //CustomVisionValuesList.Add(CustomVisionValues);
            
            return CustomVisionValues;
        }
    }
}
