using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DamageParserServer.Controllers
{
    [Route("[controller]")]
    public class RaidController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetId")]
        public IActionResult GetId()
        {
            return Json(5);
        }
    }
}