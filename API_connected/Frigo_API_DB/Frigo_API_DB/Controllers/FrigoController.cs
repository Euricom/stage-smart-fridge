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
using System.Text.RegularExpressions;

using MailKit.Net.Smtp;
using MimeKit;




//using System;
//using System.Net;
//using System.Net.Mail;
//using System.Net.Mime;
//using System.Threading;
//using System.ComponentModel;

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

        private FridgeDbContext frigoContext;
        public FrigoController(UserManager<Person> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration, FridgeDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            this.frigoContext = context;
        }



        
        // GET: api/<FrigoController>
        [HttpGet]
        public string Get()
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
            //return frigoContext.Hoeveelheden.ToList();
            //Amounts test = new Amounts(0, "test", 0);
            //return test;
            return "test";
        }

        [HttpGet("pers")]
        public List<Person> GetPerson()
        {
            return frigoContext.Persons.ToList();
        }

        // GET api/<FrigoController>/5
        [HttpGet("id")]
        public async void Get(int id)
        {
            SendMail send = new SendMail();
            string email = "matthias.hernalsteen@gmail.com";
            var tableData = frigoContext.Hoeveelheden.ToList();
            await send.Execute(email, tableData);
            //return "value";
        }

        [HttpPost("rasPi")]
        public async Task<string> Post(List<RasPiInput> dr)
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
            await email();
            return "Good";
        }
        // oplossing zoeken voor dit async gedeelte.
        private async Task email()
        {
            SendMail send = new SendMail();
            var persons = frigoContext.Persons.ToArray();
            for (int i = 0; i < persons.Count(); i++)
            {
                Settings set = frigoContext.Settings.Where(s => s.UserId == persons[i].Id).FirstOrDefault();
                if (set.WantToRecieveNotification)
                {
                    string email = set.EmailToSendTo;
                    var tableData = frigoContext.Hoeveelheden.ToList();
                    Boolean sendTheMail = false;
                    for (int j = 0; j < tableData.Count(); j++)
                    {
                        if(tableData[j].Amount <= set.SendAmount)
                        {
                            sendTheMail = true;
                            break;
                        }
                    }
                    if (sendTheMail)
                    {
                        await send.Execute(email, tableData);
                    }
                }
            }
        }




        // Normaly this isn't needed because I'll convert this in mij Raspi
        //[HttpPost("TensorFlow")]
        //public List<RasPiInput> Post(List<TensorFlowModel> Tensor)
        //{
        //    List<RasPiInput> makeOldList = new List<RasPiInput>();
        //    for (int i = 0; i < Tensor.Count(); i++)
        //    {
        //        TensorFlow Tens = new TensorFlow(Tensor[i]);
        //        RasPiInput ras = Tens.ConvertToCustomModel();
        //        makeOldList.Add(Tens.ConvertToCustomModel());
        //    }
        //    return makeOldList;
        //}


        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(PersonLoginModel login)
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
                    new Claim(ClaimTypes.NameIdentifier, userExists.Id)
                };
                //This helps to make the token with a secret key that only the server side knowa about
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtToken:secret"]));

                
                var token = new JwtSecurityToken(
                    issuer: _configuration["jwtToken:issuer"],
                    audience: _configuration["jwtToken:audience"],
                    expires: DateTime.Now.AddHours(24),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                //returned the token + how long it is valid + the userid
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    id = userExists.Id,
                    userName = userExists.UserName,
                 });
            }
            return new BadRequestObjectResult(new { message = "EmailOrPasswordIsNotCorrect" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(PersonRegisterModel register)
        {
            var userExists = await _userManager.FindByEmailAsync(register.Email);
            if (userExists != null)
            {
                return new BadRequestObjectResult(new { message = "EmailAlreadyExists" });
            }
            string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$";
            if (!Regex.IsMatch(register.Password, pattern))
            {
                return new BadRequestObjectResult(new { message = "WrongPasswordStructure" });
            }
           

            var personToAdd = new Person()
            {
                Email = register.Email
            };
            
            string username = personToAdd.makeUsername(register.FirstName, register.LastName);
            var userNameExists = await _userManager.FindByNameAsync(username);
            if(userNameExists != null)
            {
                return new BadRequestObjectResult(new { message = "UserNameAlreadyExists" });
            }
            personToAdd.UserName = username;

            var result = await _userManager.CreateAsync(personToAdd, register.Password);
            if(!result.Succeeded)
            {
                return new BadRequestObjectResult(new { message = "UserCouldnotBeMade" });
            }
            //make settings for this id
            Settings newUserSettings = new Settings();
            newUserSettings.EmailToSendTo = register.Email;
            newUserSettings.SendAmount = 5;
            newUserSettings.UserId = await _userManager.GetUserIdAsync(personToAdd);
            frigoContext.Settings.Add(newUserSettings);
            frigoContext.SaveChanges();
            return Ok();
            
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
