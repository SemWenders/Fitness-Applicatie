using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class TrainingDTO
    {
        public string trainingID { get; private set; }
        public string UserID { get; private set; }
        public string ExerciseName { get; private set; }
    }
}
