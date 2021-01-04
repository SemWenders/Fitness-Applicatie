﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            List<ExerciseDTO> exerciseDTOs = dal.GetAllExerciseDTOs();
            Console.WriteLine(exerciseDTOs.ToString());
        }
    }
}
