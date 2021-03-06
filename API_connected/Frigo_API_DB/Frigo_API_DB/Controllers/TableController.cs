﻿using Frigo_API_DB.Data;
using Frigo_API_DB.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frigo_API_DB.Controllers
{
    [Authorize]
    [Route("Table")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IConfiguration _configuration;
        private FridgeDbContext frigoContext;
        public TableController(UserManager<Person> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, FridgeDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            this.frigoContext = context;
        }

        [HttpGet("fridgeContent")]
        public TableReturnModel GetFridgeContent()
        {
            TableReturnModel values = new TableReturnModel();
            values.tableData = frigoContext.Hoeveelheden.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            values.minAmount = frigoContext.Settings.Where(s => s.UserId == userId).Select(u => u.SendAmount).SingleOrDefault();
            return values;
        }
    }
}
