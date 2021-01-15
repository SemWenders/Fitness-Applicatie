using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class RoundDTO
    {
        public ExerciseDTO Exercise { get; private set; }
        public Guid RoundID { get; private set; }
        public Guid TrainingID { get; private set; }
        public Guid ExerciseID { get; private set; }
        private List<SetDTO> sets;

        public List<SetDTO> GetSets()
        {
            return sets;
        }

        public RoundDTO(ExerciseDTO exercise, Guid roundID, Guid trainingID, Guid exerciseID, List<SetDTO> sets)
        {
            Exercise = exercise;
            RoundID = roundID;
            TrainingID = trainingID;
            ExerciseID = exerciseID;
            this.sets = sets;
        }

        public RoundDTO(ExerciseDTO exercise, Guid exerciseID, List<SetDTO> sets)
        {
            Exercise = exercise;
            ExerciseID = exerciseID;
            this.sets = sets;
        }
    }
}
