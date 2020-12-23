using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Round
    {
        public Exercise Exercise { get; private set; }
        public List<Set> Sets { get; set; }
        public Guid RoundID { get; set; }
        public Guid TrainingID { get; set; }
        public Guid ExerciseID { get; set; }
    }
}
