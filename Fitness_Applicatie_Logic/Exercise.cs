using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Exercise
    {
        public Guid ExerciseID { get; private set; }
        public string Name { get; private set; }
        public Guid UserID { get; private set; }
        public ExerciseType ExerciseType { get; private set; }

        //constructor
        public Exercise(Guid exerciseID, string name, Guid userID, ExerciseType exerciseType)
        {
            ExerciseID = exerciseID;
            Name = name;
            UserID = userID;
            ExerciseType = exerciseType;
        }
    }
}
