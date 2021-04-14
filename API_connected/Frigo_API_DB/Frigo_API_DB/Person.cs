using Frigo_API_DB.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Person : IdentityUser<int>
    {
        
        public string Password { get; set; }

        public Person ()
        {

        }
        public Person(string name, string hash)
        {
            Email = name;
            Password = hash;
        }

        public bool rightPassword(string pasHash)
        {
            if(pasHash == this.Password)
            {
                return true;
            }
            return false;
        }

        

    }
}
