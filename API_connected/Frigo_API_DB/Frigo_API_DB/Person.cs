using Frigo_API_DB.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Person : IdentityUser
    {
        public void makeUsername(string FirstName, string LastName)
        {
            this.UserName = FirstName + " " + LastName;
        }

        

    }
}
