using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Applicatie.Controllers
{
    public class TrainingController : Controller
    {
        public IActionResult AddTraining()
        {
            return View();
        }
        public IActionResult TrainingDetail()
        {
            return View();
        }
    }
}
