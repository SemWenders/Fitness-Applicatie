using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class SetDTO
    {
        public List<int> Weights { get; private set; }
        public string ExerciseID { get; private set; }
        public string TrainingID { get; private set; }
    }
}
