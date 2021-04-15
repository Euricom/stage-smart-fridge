using Frigo_API_DB.Data;
using Frigo_API_DB.DTO;
using IdentityServer3.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Frigo_API_DB.Controllers
{
    [Route("Frigo")]
    [ApiController]
    public class FrigoController : ControllerBase
    {


        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IConfiguration _configuration;
        //private readonly IJwtAuthManager _jwtAuthManager;

        private FridgeDbContext frigoContext;
        //, IJwtAuthManager jwtAuthManager
        public FrigoController(UserManager<Person> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration, FridgeDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            this.frigoContext = context;
            //_jwtAuthManager = jwtAuthManager;
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
        public async Task<IActionResult> PostLogin(PersonModel login)
        {
            var userExists = await _userManager.FindByEmailAsync(login.Email);
            if (userExists == null)
            {
                return new BadRequestObjectResult(new { message = "EmailOrPasswordIsNotCorrect" });
            }
            if (await _userManager.CheckPasswordAsync(userExists, login.Password))
            {
                //Make shure that you can find the name for who is the token.
                var authClaims = new[]
                {
                    new Claim(ClaimTypes.Email, userExists.Email),
                    //What does this piece of code
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                //This helps to make the token with a secret key that only the server side knowa about
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtToken:Secret"]));

                
                var token = new JwtSecurityToken(
                    issuer: _configuration["jwtToken:ValidIssuer"],
                    audience: _configuration["jwtToken:ValidAudience"],
                    expires: DateTime.Now.AddHours(24),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                //returned the token + how long it is valid + the userid
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    id = userExists.Id
                });
            }
            return new BadRequestObjectResult(new { message = "EmailOrPasswordIsNotCorrect" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(PersonModel register)
        {
            var userExists = await _userManager.FindByEmailAsync(register.Email);
            if (userExists != null)
            {
                return new BadRequestObjectResult(new { message = "EmailAlreadyExists" });
            }
           
            var personToAdd = new Person()
            {
                Email = register.Email
            };
            personToAdd.makeUsernameFromEmail();

            var result = await _userManager.CreateAsync(personToAdd, register.Password);
            if(!result.Succeeded)
            {
                return new BadRequestObjectResult(new { message = "UserCouldnotBeMade" });
            }
           

            return new OkObjectResult(new { message = "200 OK"});
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
