using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Settings
    {
        public string EmailToSendTo { get; set; }
        public string UserId { get; set; }
        public int SendAmount { get; set; }
        public bool WantToRecieveNotification { get; set; }
        public Settings()
        {

        }
    }
}
