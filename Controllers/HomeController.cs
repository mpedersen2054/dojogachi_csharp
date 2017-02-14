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

        // GET: /api/feed
        [HttpPost]
        [Route("/api/feed")]
        public IActionResult Feed()
        {
            string error;
            string message;
            int fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int meals = (int)HttpContext.Session.GetInt32("Meals");
            Random rand = new Random();

            // if no meals, cannot feed dachi
            if (meals < 1)
            {
                error = "true";
                message = "You do not have enough meals to do that!";
            }
            // else meal - 1, gains 5-10 fullness
            else
            {
                int rando = rand.Next(5, 11);
                int increaseBy = fullness + rando;
                HttpContext.Session.SetInt32("Meals", meals - 1);
                HttpContext.Session.SetInt32("Fullness", increaseBy);
                error = "false";
                message = $"You gained {rando} fullness!";
            }
            return Json(
                new {
                    err = error,
                    msg = message,
                    newFullness = (int)HttpContext.Session.GetInt32("Fullness"),
                    newMeals = (int)HttpContext.Session.GetInt32("Meals")
                }
            );
        }

        // GET: /api/play
        [HttpPost]
        [Route("/api/play")]
        public IActionResult Play()
        {
            return Json(
                new {
                    something = "Hello thur! PLAY",
                    another = "Hi there frendz"
                }
            );
        }

        // GET: /api/work
        [HttpPost]
        [Route("/api/work")]
        public IActionResult Work()
        {
            return Json(
                new {
                    something = "Hello thur! WORKKKK",
                    another = "Hi there frendz"
                }
            );
        }

        // GET: /api/sleep
        [HttpPost]
        [Route("/api/sleep")]
        public IActionResult Sleep()
        {
            return Json(
                new {
                    something = "Hello thur! SLEEEEEEP",
                    another = "Hi there frendz"
                }
            );
        }
    }
}
