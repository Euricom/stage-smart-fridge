using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.DTO
{
    public class PersonRegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is obligted")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is obligted")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Firstname is obligted")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is Obligated")]
        public string LastName { get; set; }
    }
}
