using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class WeightTraining : Training
    {
        private List<Round> rounds;

        public List<Round> GetRounds()
        {
            return rounds;
        }

        public WeightTraining(List<Round> rounds, Guid trainingID, Guid userID, DateTime date, TrainingType trainingType) : base(trainingID, userID, date, trainingType)
        {
            this.rounds = rounds;
        }
    }
}
