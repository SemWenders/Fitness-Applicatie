using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Applicatie.Models
{
    public class RoundViewModel
    {
        public List<SetViewModel> Sets { get; set; }
        public ExerciseViewModel Exercise { get; set; }
        public Guid RoundID { get; set; }
        public Guid TrainingID { get; set; }
        public Guid ExerciseID { get; set; }
    }
}
