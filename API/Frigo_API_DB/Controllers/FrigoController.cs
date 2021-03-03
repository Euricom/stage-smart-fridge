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
        // GET: api/<FrigoController>
        [HttpGet]
        public List<hoeveelheid> Get()
        {
            hoeveelheid cola = new hoeveelheid(1,"Cola", 7);
            hoeveelheid fanta = new hoeveelheid(2, "Fanta", 15);
            hoeveelheid sprite = new hoeveelheid(3, "Sprite", 0);
            List<hoeveelheid> aantallen = new List<hoeveelheid>();
            aantallen.Add(cola);
            aantallen.Add(fanta);
            aantallen.Add(sprite);
            return aantallen;
        }

        // GET api/<FrigoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public string Post(List<DrankP> dr)
        {
            // klasse bereken aanmaken en hierin de data doorsturen die ik wil uitrekenen.
            return "Joepie";
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
