using Frigo_API_DB.Data;
using Frigo_API_DB.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.Controllers
{
    [Authorize]
    [Route("Settings")]
    [ApiController]
    public class SettingsController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IConfiguration _configuration;
        private FridgeDbContext frigoContext;

        public SettingsController(UserManager<Person> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, FridgeDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            this.frigoContext = context;
        }

        [HttpPost("getSettings")]
        public List<Settings> GetSettings(string id)
        {
            return frigoContext.Settings.Where(s => s.UserId == id).ToList();
        }

        [HttpPost("setSettings")]
        public IActionResult SetSettings(SettingsModel newSettings)
        {
            Settings set = frigoContext.Settings.Single(c => c.UserId == newSettings.UserId);
            set.SendAmount = newSettings.SendAmount;
            set.EmailToSendTo = newSettings.EmailToSendTo;
            set.WantToRecieveNotification = newSettings.WantToRecieveNotification;
            frigoContext.SaveChanges();
            return Ok();
        }
    }
}
