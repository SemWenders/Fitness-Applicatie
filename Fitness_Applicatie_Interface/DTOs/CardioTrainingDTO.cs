using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class CardioTrainingDTO : TrainingDTO
    {
        public ExerciseDTO Exercise { get; set; }
        public decimal Distance { get; set; }
        public TimeSpan Time { get; set; }
    }
}
