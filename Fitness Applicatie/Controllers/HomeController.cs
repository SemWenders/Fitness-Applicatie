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
                Trainings = user.GetTrainings(),
                UserID = user.UserID.ToString()
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
            User user = new User(null, Guid.Empty, null, null, null);
            TrainingViewModel trainingViewModel = new TrainingViewModel();
            Training training = user.GetTraining(id);

            if (training.TrainingType == TrainingType.Strength)
            {
                WeightTraining weightTraining = user.GetWeightTraining(id);
                trainingViewModel.Rounds = weightTraining.GetRounds();
                trainingViewModel.Date = weightTraining.Date;
                trainingViewModel.TrainingID = weightTraining.TrainingID;
                trainingViewModel.TrainingType = weightTraining.TrainingType;
            }
            else if (training.TrainingType == TrainingType.Cardio)
            {
                CardioTraining cardioTraining = user.GetCardioTraining(id);
                trainingViewModel.Exercise = cardioTraining.Exercise;
                trainingViewModel.Distance = cardioTraining.Distance;
                trainingViewModel.Time = cardioTraining.Time;
                trainingViewModel.TrainingID = cardioTraining.TrainingID;
                trainingViewModel.TrainingType = cardioTraining.TrainingType;
            }
            return View("../Training/TrainingDetail", trainingViewModel);
        }
    }
}
