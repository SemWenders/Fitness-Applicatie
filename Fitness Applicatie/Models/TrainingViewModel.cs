using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Applicatie.Models
{
    public class TrainingViewModel
    {
        public List<Models.ExerciseViewModel> Name { get; set; }
        public List<int> Sets { get; set; }
    }
}
