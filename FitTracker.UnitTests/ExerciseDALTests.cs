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
            User user = new User("Admin", Guid.NewGuid(), "TestPassword", null, null);
            Exercise exercise = new Exercise(Guid.NewGuid(), "Benchpress", user.UserID, ExerciseType.Weighted);

            Interface.Interfaces.IExerciseDAL dal = Factory.ExerciseFactory.GetExerciseDAL();
            ExerciseDTO exerciseDTO = new ExerciseDTO(exercise.ExerciseID, exercise.Name, exercise.UserID, (ExerciseTypeDTO)exercise.ExerciseType);
            dal.AddExercise(exerciseDTO);
        }

        [TestMethod]
        public void GetExercises()
        {
            Interface.Interfaces.IExerciseDAL dal = Factory.ExerciseFactory.GetExerciseDAL();
            List<ExerciseDTO> exerciseDTOs = dal.GetAllExerciseDTOs();
            Console.WriteLine(exerciseDTOs.ToString());
        }
    }
}
