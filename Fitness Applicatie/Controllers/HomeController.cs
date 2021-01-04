using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fitness_Applicatie.Models;
using FitTracker.Logic;
using Microsoft.AspNetCore.Authorization;

namespace Fitness_Applicatie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            UserCollection userCollection = new UserCollection();
            User user = userCollection.GetUser(User.FindFirst("Id").Value);
            UserViewModel userViewModel = new UserViewModel
            {
                Name = user.Name,
                Trainings = user.Trainings,
                UserID = user.UserID
            };
            return View(userViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TrainingDetail(string id)
        {
            User user = new User();


            return View("../Training/TrainingDetail", trainingViewModel);
        }
    }
}
