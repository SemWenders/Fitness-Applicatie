using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Logic;
using LogicFactory;
using FitTracker.LogicInterface;
using FitTracker.LogicFactory;
using Fitness_Applicatie.Controllers;
using Fitness_Applicatie.Models;
using FitTracker.Interface.Interfaces;
using FitTracker.Factory;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class ExerciseTests
    {
        Guid userID = Guid.Parse("94E1E099-538F-4E9E-830A-04952A2DD682");
        [ClassCleanup]
        public static void CleanUpTests()
        {
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            ExerciseDTO tempDeadlift = userCollection.GetExercise("TempDeadlift");
            ExerciseDTO tempSquat = userCollection.GetExercise("TempSquat");
            ExerciseDTO tempPushup = userCollection.GetExercise("TempPushup");
            ExerciseDTO tempLegPress = userCollection.GetExercise("TempLegpress");
            ExerciseDTO tempLegextenstion = userCollection.GetExercise("TempLegextension");
            userCollection.DeleteExercise(tempDeadlift.ExerciseID.ToString());
            userCollection.DeleteExercise(tempSquat.ExerciseID.ToString());
            userCollection.DeleteExercise(tempPushup.ExerciseID.ToString());
            userCollection.DeleteExercise(tempLegPress.ExerciseID.ToString());
            userCollection.DeleteExercise(tempLegextenstion.ExerciseID.ToString());
        }

        [TestMethod]
        public void AddExercise_Correctly()
        {
            //arrange
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            IUser user = UserFactory.GetUser();
            ExerciseDTO legpress = new ExerciseDTO("TempLegpress", userID, ExerciseTypeDTO.Weighted);

            //act
            user.AddExercise(legpress);

            //assert
            ExerciseDTO legpressDTO = userCollection.GetExercise("TempLegpress");

            Assert.AreEqual("TempLegpress", legpressDTO.Name);
            Assert.AreEqual(ExerciseTypeDTO.Weighted, legpressDTO.ExerciseType);
            Assert.AreEqual(userID, legpressDTO.UserID);
        }

        [TestMethod]
        public void Gets_two_exercises_successfully()
        { 
            // Arrange
            IUser user = UserFactory.GetUser();
            IExerciseDAL dal = ExerciseDALFactory.GetExerciseDAL();
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            ExerciseDTO legextension = new ExerciseDTO(Guid.NewGuid(), "TempLegExtension", userID, ExerciseTypeDTO.Weighted);
            ExerciseDTO pushup = new ExerciseDTO(Guid.NewGuid(), "TempPushup", userID, ExerciseTypeDTO.Bodyweight);

            dal.AddExercise(legextension);
            dal.AddExercise(pushup);

            // Act
            ExerciseDTO legextensionDTO = userCollection.GetExercise("TempLegextension");
            ExerciseDTO pushupDTO = userCollection.GetExercise("TempPushup");


            // Assert 
            Assert.AreEqual(userID, legextensionDTO.UserID);
            Assert.AreEqual(ExerciseTypeDTO.Weighted, legextensionDTO.ExerciseType);
            Assert.AreEqual("TempLegextension", legextensionDTO.Name);
            Assert.AreEqual(legextension.ExerciseID, legextensionDTO.ExerciseID);

            Assert.AreEqual(userID, pushupDTO.UserID);
            Assert.AreEqual(ExerciseTypeDTO.Bodyweight, pushupDTO.ExerciseType);
            Assert.AreEqual("TempPushup", pushupDTO.Name);
            Assert.AreEqual(pushup.ExerciseID, pushupDTO.ExerciseID);
        }

        [TestMethod]
        public void Get_Exercise_By_ID()
        {
            //arrange
            IUser user = UserFactory.GetUser();
            IExerciseDAL dal = ExerciseDALFactory.GetExerciseDAL();
            ExerciseDTO tempExercise = new ExerciseDTO(Guid.NewGuid(), "TempSquat", userID, ExerciseTypeDTO.Weighted);
            dal.AddExercise(tempExercise);

            //act
            ExerciseDTO exerciseDTO = user.GetExercise(tempExercise.ExerciseID.ToString());

            //assert
            Assert.AreEqual("TempSquat", exerciseDTO.Name);
            Assert.AreEqual(tempExercise.ExerciseID, exerciseDTO.ExerciseID);
            Assert.AreEqual(ExerciseTypeDTO.Weighted, exerciseDTO.ExerciseType);
            Assert.AreEqual(userID, exerciseDTO.UserID);
        }


        [TestMethod]
        public void Get_Exercise_By_Name()
        {
            //arrange
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            IExerciseDAL dal = ExerciseDALFactory.GetExerciseDAL();
            Guid exerciseID = Guid.NewGuid();
            ExerciseDTO tempExercise = new ExerciseDTO(exerciseID, "TempDeadlift", userID, ExerciseTypeDTO.Weighted);
            dal.AddExercise(tempExercise);

            //act
            ExerciseDTO TempDeadliftDTO = userCollection.GetExercise("TempDeadlift");

            //assert
            Assert.AreEqual("TempDeadlift", TempDeadliftDTO.Name);    
            Assert.AreEqual(exerciseID, TempDeadliftDTO.ExerciseID);    
            Assert.AreEqual(ExerciseTypeDTO.Weighted, TempDeadliftDTO.ExerciseType);    
            Assert.AreEqual(userID, TempDeadliftDTO.UserID);
        }
    }
}
