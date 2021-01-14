using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Logic;
using LogicFactory;
using FitTracker.LogicInterface;
using FitTracker.LogicFactory;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class ExerciseTests
    {
        [TestMethod]
        public void AddExercise_Correctly()
        {
            Exercise exercise = new Exercise(Guid.NewGuid(), "Legpress", Guid.Parse("94E1E099-538F-4E9E-830A-04952A2DD682"), ExerciseType.Weighted);
            IUser user = UserFactory.GetUser();
            ExerciseDTO exerciseDTO = new ExerciseDTO(exercise.ExerciseID, exercise.Name, exercise.UserID, (ExerciseTypeDTO)exercise.ExerciseType);
            user.AddExercise(exerciseDTO);
        }

        [TestMethod]
        public void GetExercises()
        {
            Interface.Interfaces.IExerciseDAL dal = Factory.ExerciseDALFactory.GetExerciseDAL();
            List<ExerciseDTO> exerciseDTOs = dal.GetAllExerciseDTOs();
        }

        [TestMethod]
        public void GetExercise()
        {
            IUser user = UserFactory.GetUser();
            ExerciseDTO exerciseDTO = user.GetExercise("4C619880-E337-476F-80F2-1971A2A31BF8");
            Assert.AreEqual("Deadlift", exerciseDTO.Name);
        }


        [TestMethod]
        public void GetExerciseByName()
        {
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            ExerciseDTO exerciseDTO = userCollection.GetExercise("Deadlift");
            Assert.AreEqual(Guid.Parse("4C619880-E337-476F-80F2-1971A2A31BF8"), exerciseDTO.ExerciseID);    
        }
    }
}
