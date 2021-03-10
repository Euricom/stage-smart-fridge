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
        public List<Hoeveelheid> Get()
        {
            // Voor de demo
            Hoeveelheid cola = new Hoeveelheid(1, "Cola", 7);
            Hoeveelheid fanta = new Hoeveelheid(2, "Fanta", 15);
            Hoeveelheid sprite = new Hoeveelheid(3, "Sprite", 0);
            List<Hoeveelheid> aantallen = new List<Hoeveelheid>();
            aantallen.Add(cola);
            aantallen.Add(fanta);
            aantallen.Add(sprite);
            //return aantallen;

            // Voor met de database te werken
            return frigoContext.Hoeveelheden.ToList();
        }

        // GET api/<FrigoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public List<DrankP> Post(List<DrankP> dr)
        {
            // data opsturen naar de berekeningen, uitrekenen en dan met de list data opslaan.
            Bereken rekenen = new Bereken();
            //List<Hoeveelheid> aantallen = rekenen.Counter(dr);

            //for(int i = 0; i < aantallen.Count(); i++)
            //{

            //}
            Hoeveelheid cola = frigoContext.Hoeveelheden.Single(c => c.Id == 1);
            cola.Aantal = 10;

            Hoeveelheid fanta = frigoContext.Hoeveelheden.Single(c => c.Id == 2);
            fanta.Aantal = 10;
            frigoContext.SaveChanges();
            return dr;
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
