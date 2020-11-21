using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Applicatie.Models
{
    public class ExerciseViewModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public bool BodyWeight { get; set; }
    }
}
