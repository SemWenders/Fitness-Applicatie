using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.LogicInterface
{
    public interface IExercise
    {
        public bool ExerciseExists(string exerciseName);
        public ExerciseDTO GetExerciseByName(string exerciseName);
    }
}
