using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FitTracker.Logic;
using FitTracker.Interface.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Applicatie.Controllers
{
    public class ExerciseController : Controller
    {
        [Authorize]
        public IActionResult AddStrengthExercise()
        {
            return View();
        }

        public ActionResult AddExerciseToDb(Models.ExerciseViewModel exercise)
        {
            return LocalRedirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult AddStrengthExercise(Models.ExerciseViewModel exerciseViewModel)
        {
            try
            {
                User user = new User();
                Exercise exercise = new Exercise();
                ExerciseDTO exerciseDTO = new ExerciseDTO(Guid.NewGuid(), exerciseViewModel.Name, Guid.Parse(User.FindFirst("Id").Value), (ExerciseTypeDTO)exerciseViewModel.ExerciseType);
                if (String.IsNullOrEmpty(exerciseViewModel.Name) || exerciseViewModel.ExerciseType.ToString() == "Empty")
                {
                    ModelState.AddModelError("Name", "Fill in all fields please");
                    return View(exerciseViewModel);
                }
                if (exercise.ExerciseExists(exerciseViewModel.Name))
                {
                    ModelState.AddModelError("Name", "Exercise already exists");
                    return View(exerciseViewModel);
                }
                else
                {
                    user.AddExercise(exerciseDTO);
                    TempData["JustAddedExercise"] = true;
                }
                return LocalRedirect("/Home/Index");
            }
            catch
            {
                TempData["Error"] = true;
                return LocalRedirect("/Home/Index");
            }
        }
    }
}
