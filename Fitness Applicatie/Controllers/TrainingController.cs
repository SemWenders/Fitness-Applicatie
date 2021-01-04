using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
