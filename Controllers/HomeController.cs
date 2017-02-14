using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("DachiIndex");
        }

        // GET: /
        [HttpGet]
        [Route("/dachi")]
        public IActionResult DachiIndex()
        {
            return View("Dachi");
        }
    }
}
