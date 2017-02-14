using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {

        // private T IsGameOver<T>(int fullness, int happiness, int energy)
        // {
        //     Tuple<bool,string> resp = new Tuple<bool,string>();
        //     // handle win cond
        //     if (fullness > 99)
        //     {

        //     }

        //     // handle lose cond
        //     return ;
        // }

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
                // initialize session vars
                HttpContext.Session.SetString("Playing", "true");
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
            }
            // set initial data, updates will be handled by ajax
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
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

            // GameOver();

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
                message = $"You fed your Dojogachi! Fullness + {rando}, Meals - 1";
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
            string error;
            string message;
            int energy = (int)HttpContext.Session.GetInt32("Energy");
            int happiness = (int)HttpContext.Session.GetInt32("Happiness");
            Random rand = new Random();

            // not enough energy
            if (energy < 5)
            {
                error = "true";
                message = "You do not have enough energy to do that!";
            }
            else
            {
                // - 5 energy, gain rand # 5-10 happiness
                int rando = rand.Next(5, 11);
                int increaseBy = happiness + rando;
                HttpContext.Session.SetInt32("Energy", energy - 5);
                HttpContext.Session.SetInt32("Happiness", increaseBy);
                error = "false";
                message = $"You played with your Dojogachi! Happiness + {rando}, Energy - 5";
            }

            return Json(
                new {
                    err = error,
                    msg = message,
                    newHappiness = (int)HttpContext.Session.GetInt32("Happiness"),
                    newEnergy = (int)HttpContext.Session.GetInt32("Energy")
                }
            );
        }

        // GET: /api/work
        [HttpPost]
        [Route("/api/work")]
        public IActionResult Work()
        {
            string error;
            string message;
            int energy = (int)HttpContext.Session.GetInt32("Energy");
            int meals = (int)HttpContext.Session.GetInt32("Meals");
            Random rand = new Random();

            // -5 energy, + 1-3 meals
            if (energy < 5)
            {
                error = "true";
                message = "You do not have enough energy to do that!";
            }
            else
            {
                int rando = rand.Next(1, 4);
                int increaseBy = rando + meals;
                HttpContext.Session.SetInt32("Energy", energy - 5);
                HttpContext.Session.SetInt32("Meals", increaseBy);
                error = "false";
                message = $"You played with your Dojogachi! Meals + {rando}, Energy - 5";
            }

            return Json(
                new {
                    err = error,
                    msg = message,
                    newEnergy = (int)HttpContext.Session.GetInt32("Energy"),
                    newMeals = (int)HttpContext.Session.GetInt32("Meals")
                }
            );
        }

        // GET: /api/sleep
        [HttpPost]
        [Route("/api/sleep")]
        public IActionResult Sleep()
        {
            string error;
            string message;
            int fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int happiness = (int)HttpContext.Session.GetInt32("Happiness");
            int energy = (int)HttpContext.Session.GetInt32("Energy");

            if (happiness < 5)
            {
                error = "true";
                message = $"You do not have enough happiness to do that!";
            }
            else if (fullness < 5)
            {
                error = "true";
                message = $"You do not have fullness energy to do that!";
            }
            else
            {
                // +15 energy, -5 fullness -5 happiness
                HttpContext.Session.SetInt32("Fullness", fullness - 5);
                HttpContext.Session.SetInt32("Happiness", happiness - 5);
                HttpContext.Session.SetInt32("Energy", energy + 15);
                error = "false";
                message = "Your Dojogachi fell asleep! Energy + 15, Fullness - 5, Happiness - 5";
            }
            return Json(
                new {
                    err = error,
                    msg = message,
                    newFullness = (int)HttpContext.Session.GetInt32("Fullness"),
                    newHappiness = (int)HttpContext.Session.GetInt32("Happiness"),
                    newEnergy = (int)HttpContext.Session.GetInt32("Energy")
                }
            );
        }
    }
}
