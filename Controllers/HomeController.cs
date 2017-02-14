using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}

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
            if (HttpContext.Session.GetString("Playing") != "true")
            {
                HttpContext.Session.SetString("Playing", "true");
                System.Console.WriteLine("HELLO FROM INSIDE IF STATE");
                // List<int> dachiData = new List<int>() {};
                // HttpContext.Session.SetObjectAsJson("DachiData", dachiData);
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
            }
            
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");

            // List<int> retData = HttpContext.Session.GetObjectFromJson("DachiData");
            // System.Console.WriteLine(retData);
            
            return View("Dachi");
        }

        // GET: /
        [HttpPost]
        [Route("/api/feed")]
        public IActionResult Feed()
        {
            return Json(
                new {
                    something = "Hello thur!",
                    another = "Hi there frendz"
                }
            );
        }
    }
}
