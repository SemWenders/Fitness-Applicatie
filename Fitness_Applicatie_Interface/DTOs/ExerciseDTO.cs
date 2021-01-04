using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class ExerciseDTO
    {
        public Guid ExerciseID { get; private set; }
        public string Name { get; private set; }
        public Guid UserID { get; private set; }
        public ExerciseTypeDTO ExerciseType { get; private set; }

        public ExerciseDTO(Guid exerciseID, string name, Guid userID, ExerciseTypeDTO exerciseType)
        {
            ExerciseID = exerciseID;
            Name = name;
            UserID = userID;
            ExerciseType = exerciseType;
        }

        public ExerciseDTO()
        {

        }
    }
}
