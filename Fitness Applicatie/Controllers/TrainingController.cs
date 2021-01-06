using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Applicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTracker.Logic;

namespace Fitness_Applicatie.Controllers
{
    public class TrainingController : Controller
    {
        [Authorize]
        public IActionResult AddTraining()
        {
            return View();
        }
        [Authorize]
        public IActionResult TrainingDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStrengthTraining(TrainingViewModel trainingViewModel)
        {
            List<Round> rounds = new List<Round>();
            foreach (var roundViewModel in trainingViewModel.Rounds)
            {
                rounds.Add(ConvertRoundVM(roundViewModel));
            }
            WeightTraining weightTraining = new WeightTraining(rounds, Guid.NewGuid(), Guid.Parse(User.FindFirst("Id").Value), DateTime.Now, TrainingType.Strength);

            return View();
        }

        private Round ConvertRoundVM(RoundViewModel roundViewModel)
        {
            List<Set> sets = new List<Set>();
            foreach (var set in roundViewModel.Sets)
            {
                sets.Add(ConvertSetVM(set));
            }
            Round round = new Round(ConvertExerciseVM(roundViewModel.Exercise), sets, roundViewModel.RoundID, roundViewModel.TrainingID, roundViewModel.ExerciseID);
            return round;
        }

        private Set ConvertSetVM(SetViewModel setViewModel)
        {
            Set set = new Set(setViewModel.Repitions, setViewModel.Weight, setViewModel.SetOrder, setViewModel.SetID);
            return set;
        }

        private Exercise ConvertExerciseVM(ExerciseViewModel exerciseViewModel)
        {
            Exercise exercise = new Exercise(exerciseViewModel.ExerciseID, exerciseViewModel.Name, exerciseViewModel.UserID, exerciseViewModel.ExerciseType);
            return exercise;
        }
    }
}
