using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Logic;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class ExerciseDALTests
    {
        [TestMethod]
        public void AddExercise_Correctly()
        {
            User user = new User
            {
                UserID = Guid.NewGuid()
            };
            Exercise exercise = new Exercise("Deadlift", false, user.UserID);
            user.AddExercise(exercise);
        }
    }
}
