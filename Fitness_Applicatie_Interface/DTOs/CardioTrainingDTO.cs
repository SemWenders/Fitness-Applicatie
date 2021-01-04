using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class CardioTrainingDTO : TrainingDTO
    {
        public ExerciseDTO Exercise { get; private set; }
        public decimal Distance { get; private set; }
        public TimeSpan Time { get; private set; }

        public CardioTrainingDTO(ExerciseDTO exercise, decimal distance, TimeSpan time, Guid trainingID, Guid userID, DateTime date, TrainingTypeDTO trainingTypeDTO) : base(trainingID, userID, date, trainingTypeDTO)
        {
            Exercise = exercise;
            Distance = distance;
            Time = time;
        }
    }
}
