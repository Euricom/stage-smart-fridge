using Frigo_API_DB.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Frigo_API_DB.Controllers
{
    [Route("Frigo")]
    [ApiController]
    public class FrigoController : ControllerBase
    {
        private FridgeDbContext frigoContext;
        public FrigoController(FridgeDbContext context)
        {
            this.frigoContext = context;
        }
        // GET: api/<FrigoController>
        [HttpGet]
        public List<Amounts> Get()
        {
            // Voor de demo
            Amounts cola = new Amounts(1, "Cola", 7);
            Amounts fanta = new Amounts(2, "Fanta", 15);
            Amounts sprite = new Amounts(3, "Sprite", 0);
            List<Amounts> aantallen = new List<Amounts>();
            aantallen.Add(cola);
            aantallen.Add(fanta);
            aantallen.Add(sprite);
            //return aantallen;

            // Voor met de database te werken
            return frigoContext.Hoeveelheden.ToList();
            //Amounts test = new Amounts(0, "test", 0);
            //return test;
        }

        [HttpGet("pers")]
        public List<Person> GetPerson()
        {
            return frigoContext.Persons.ToList();
        }

        // GET api/<FrigoController>/5
        [HttpGet("id")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("rasPi")]
        public string Post(List<RasPiInput> dr)
        {
            // data opsturen naar de berekeningen, uitrekenen en dan met de list data opslaan.

            Calculating rekenen = new Calculating();
            List<Amounts> aantallen = rekenen.Counter(dr);

            

            if (aantallen[0].Id == 0)
            {
                return aantallen[0].Name;
            }
            for (int i = 0; i < aantallen.Count(); i++)
            {
                Amounts drankje = frigoContext.Hoeveelheden.Single(c => c.Id == aantallen[i].Id);
                drankje.Amount = aantallen[i].Amount;
                frigoContext.SaveChanges();
            }


            return "Good";
        }


        [HttpPost("login")]
        public bool PostLogin(Person login)
        {
            string pasHash = frigoContext.Persons.Where(p => p.Email == login.Email).Select(p => p.PasswordHash).SingleOrDefault();
            
            return login.rightPassword(pasHash);
        }

        [HttpPost("register")]
        public bool PostRegister(Person register)
        {
            int alreadyMoreThanOne = frigoContext.Persons.Where(p => p.Email == register.Email).Count();
            if (alreadyMoreThanOne > 1)
            {
                return false;
            }
            var personExistingCheck = frigoContext.Persons.Where(p => p.Email == register.Email).SingleOrDefault();
            
            if (personExistingCheck != null)
            {
                return false;
            }
            frigoContext.Persons.Add(register);
            frigoContext.SaveChanges();
            return true;
        }



        // PUT api/<FrigoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FrigoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
