using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class CardioTraining : Training
    {
        public Exercise Exercise { get; private set; }
        public decimal Distance { get; private set; }
        public TimeSpan Time { get; private set; }

        //constructor
        public CardioTraining(Exercise exercise, decimal distance, TimeSpan time, Guid trainingID, Guid userID, DateTime date, TrainingType trainingType) : base(trainingID, userID, date, trainingType)
        {
            Exercise = exercise;
            Distance = distance;
            Time = time;
        }
    }
}