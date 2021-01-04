using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Round
    {
        public Exercise Exercise { get; private set; }
        public Guid RoundID { get; private set; }
        public Guid TrainingID { get; private set; }
        public Guid ExerciseID { get; private set; }
        private List<Set> sets;

        //constructor
        public Round(Exercise exercise, List<Set> sets, Guid roundID, Guid trainingID, Guid exerciseID)
        {
            Exercise = exercise;
            RoundID = roundID;
            TrainingID = trainingID;
            ExerciseID = exerciseID;
            this.sets = sets;
        }

        //methods
        public List<Set> GetSets()
        {
            return sets;
        }
    }
}
