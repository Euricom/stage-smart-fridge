using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.DTO
{
    public class PersonLoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is obligted")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is obligted")]
        public string Password { get; set; }

        
    }
}
