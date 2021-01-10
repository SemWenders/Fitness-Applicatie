using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Persistence;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Factory
{
    public static class ExerciseDALFactory
    {
        public static IExerciseDAL GetExerciseDAL()
        {
            return new ExerciseDAL();
        }
    }
}
