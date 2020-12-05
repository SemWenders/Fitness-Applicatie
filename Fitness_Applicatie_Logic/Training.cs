using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    class Training
    {
        public string trainingID { get; private set; }
        public string UserID { get; private set; }
        public List<Exercise> ExercisesNames { get; private set; }
        public DateTime Date { get; private set; }
        public List<Set> Sets { get; private set; } //list of weight of each set
    }
}
