using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Applicatie.Controllers
{
    public class ExerciseController : Controller
    {
        public IActionResult AddExercise()
        {
            return View();
        }

        public ActionResult AddExerciseToDb(Models.ExerciseViewModel exercise)
        {
            return LocalRedirect("/Home/Index");
        }
    }
}
