using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Persistence;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Factory
{
    static class ExerciseFactory
    {
        static IExerciseDAL GetExerciseDAL()
        {
            return new ExerciseDAL;
        }
    }
}
