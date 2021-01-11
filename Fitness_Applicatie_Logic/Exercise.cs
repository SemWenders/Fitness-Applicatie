using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using FitTracker.Factory;
using FitTracker.LogicInterface;

namespace FitTracker.Logic
{
    public class Exercise : IExercise
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

        public Exercise()
        {

        }

        //methods
        public bool ExerciseExists(string exerciseName)
        {
            IExerciseDAL dal = ExerciseDALFactory.GetExerciseDAL();
            return dal.ExerciseExists(exerciseName);
        }

        public ExerciseDTO GetExerciseByName(string exerciseName)
        {
            IExerciseDAL dal = ExerciseDALFactory.GetExerciseDAL();
            return dal.GetExerciseDTOByName(exerciseName);
        }
    }
}
