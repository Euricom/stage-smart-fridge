using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.DTO
{
    public class SettingsModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is obligted")]
        public string EmailToSendTo { get; set; }

        [Required(ErrorMessage = "UserId is obligated")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "SendAmount is obligted")]
        public int SendAmount { get; set; }
        [Required(ErrorMessage = "Checkbox is obligted")]
        public bool WantToRecieveNotification { get; set; }
    }
}
