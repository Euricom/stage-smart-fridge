using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public bool Register { get; set; }

        public Person ()
        {

        }
        public Person(string name, string hash)
        {
            Name = name;
            PasswordHash = hash;
        }

        public bool rightPassword(List<Person> createdPersons)
        {
            for(int i = 0; i < createdPersons.Count(); i++)
            {
                if(createdPersons[i].Name == Name)
                {
                    if(createdPersons[i].PasswordHash == PasswordHash)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

    }
}
