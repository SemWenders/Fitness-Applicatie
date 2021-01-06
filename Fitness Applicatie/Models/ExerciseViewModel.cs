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
        public Guid ExerciseID { get; set; }
        public string Name { get; set; }
        public Guid UserID { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
