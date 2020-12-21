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
                UserID = "TestAccount"
            };
            Exercise exercise = new Exercise
            {
                Name = "Squat",
                ExerciseID = Guid.NewGuid(),
                ExerciseType = ExerciseType.Weighted,
                UserID = user.UserID
            };

            Interface.Interfaces.IExerciseDAL dal = Factory.ExerciseFactory.GetExerciseDAL();
            ExerciseDTO exerciseDTO = new ExerciseDTO()
            {
                ExerciseID = exercise.ExerciseID,
                ExerciseType = (ExerciseTypeDTO)Enum.Parse(typeof(ExerciseTypeDTO), exercise.ExerciseType.ToString()),
                Name = exercise.Name,
                UserID = exercise.UserID
            };
            dal.AddExercise(exerciseDTO);
        }

        [TestMethod]
        public void GetExercises()
        {
            Interface.Interfaces.IExerciseDAL dal = Factory.ExerciseFactory.GetExerciseDAL();
            List<ExerciseDTO> exerciseDTOs = dal.GetExerciseDTOs();
            Console.WriteLine(exerciseDTOs.ToString());
        }
    }
}
