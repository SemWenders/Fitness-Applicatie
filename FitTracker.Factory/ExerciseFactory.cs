using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Persistence;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Factory
{
    public static class ExerciseFactory
    {
        public static IExerciseDAL GetExerciseDAL()
        {
            return new ExerciseDAL();
        }
    }
}
