using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class CardioTrainingDTO : TrainingDTO
    {
        public ExerciseDTO exercise { get; private set; }
        public int Distance { get; private set; }
        public TimeSpan time { get; private set; }
    }
}
