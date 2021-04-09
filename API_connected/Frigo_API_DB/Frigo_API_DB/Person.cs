using Frigo_API_DB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Person
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Person ()
        {

        }
        public Person(string name, string hash)
        {
            Email = name;
            PasswordHash = hash;
        }

        public bool rightPassword(string pasHash)
        {
            if(pasHash == this.PasswordHash)
            {
                return true;
            }
            return false;
        }

        

    }
}
