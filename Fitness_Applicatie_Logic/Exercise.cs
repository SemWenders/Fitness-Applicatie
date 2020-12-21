using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Exercise
    {
        public Guid ExerciseID { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
