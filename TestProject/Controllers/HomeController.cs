using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core.Models.ViewModel;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("Index")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            return StatusCode(StatusCodes.Status200OK, new ResponseVM { Status = "Ok", Message = "The TestProject is Up and is Running." });
        }
    }
}
