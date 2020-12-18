using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Round
    {
        public Exercise Exercise { get; private set; }
        public List<Set> Sets { get; private set; }
        public Guid RoundID { get; private set; }
        public Guid TrainingID { get; set; }
        public Guid ExerciseID { get; private set; }
    }
}
