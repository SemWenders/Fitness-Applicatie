using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using FitTracker.LogicFactory;
using LogicFactory;
using FitTracker.Factory;
using FitTracker.LogicInterface;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class TrainingTests
    {
        [ClassCleanup]
        public static void CleanTests()
        {
            IUserCollectionDAL userCollectionDAL = UserCollectionDALFactory.GetUserCollectionDAL();
            ITrainingDAL trainingDAL = TrainingDALFactory.GetTrainingDAL();

            UserDTO strengthAccount = userCollectionDAL.GetUser("TempAccountWeightTraining");
            UserDTO cardioAccount = userCollectionDAL.GetUser("TempAccountCardioTraining");

            Guid strengthtrainingID = trainingDAL.GetUserTrainings(strengthAccount.UserID.ToString())[0].TrainingID;
            Guid cardiotrainingID = trainingDAL.GetUserTrainings(cardioAccount.UserID.ToString())[0].TrainingID;

            trainingDAL.DeleteWeightTraining(strengthtrainingID.ToString());
            trainingDAL.DeleteCardioTraining(cardiotrainingID.ToString());

            userCollectionDAL.DeleteUser(strengthAccount.Name.ToString());
            userCollectionDAL.DeleteUser(cardioAccount.Name.ToString());
        }
        [TestMethod]
        public void AddWeightTraining()
        {
            //arrange
            IUser user = UserFactory.GetUser();
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            ITrainingDAL dal = TrainingDALFactory.GetTrainingDAL();
            ExerciseDTO deadlift = userCollection.GetExercise("Deadlift");
            ExerciseDTO squat = userCollection.GetExercise("Squat");
            ExerciseDTO pullup = userCollection.GetExercise("Pullup");
            List<SetDTO> deadliftSets = new List<SetDTO>
            {
                new SetDTO(80, 0),
                new SetDTO(85, 1),
                new SetDTO(90, 2)
            };

            List<SetDTO> squatSets = new List<SetDTO>
            {
                new SetDTO(50, 0),
                new SetDTO(55, 1),
                new SetDTO(60, 2)
            };

            List<SetDTO> pullupSets = new List<SetDTO>
            {
                new SetDTO(7, 0),
                new SetDTO(7, 1),
                new SetDTO(7, 2)
            };

            List<RoundDTO> rounds = new List<RoundDTO>
            {
                new RoundDTO(deadlift, deadlift.ExerciseID, deadliftSets),
                new RoundDTO(squat, squat.ExerciseID, squatSets),
                new RoundDTO(pullup, pullup.ExerciseID, pullupSets)
            };

            UserDTO userDTO = new UserDTO("TempAccountWeightTraining", Guid.NewGuid(), "TempPassword", null, null);
            IUserCollectionDAL userCollectionDAL = UserCollectionDALFactory.GetUserCollectionDAL();
            userCollectionDAL.AddUser(userDTO);

            WeightTrainingDTO weightTrainingDTO = new WeightTrainingDTO(rounds, userDTO.UserID, DateTime.Now, TrainingTypeDTO.Strength);

            //act
            user.AddStrengthTraining(weightTrainingDTO);

            //assert
            Guid trainingID = dal.GetUserTrainings(userDTO.UserID.ToString())[0].TrainingID;
            WeightTrainingDTO trainingFromDB = dal.GetWeightTraining(trainingID.ToString());

            Assert.AreEqual(userDTO.UserID, trainingFromDB.UserID);
            Assert.AreEqual(weightTrainingDTO.Date.ToLongDateString(), trainingFromDB.Date.ToLongDateString());
            Assert.AreEqual(TrainingTypeDTO.Strength, trainingFromDB.TrainingType);
        }

        [TestMethod]
        public void AddCardioTraining()
        {
            //arrange
            IUser user = UserFactory.GetUser();
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            IUserCollectionDAL userCollectionDAL = UserCollectionDALFactory.GetUserCollectionDAL();
            ITrainingDAL dal = TrainingDALFactory.GetTrainingDAL();
            ExerciseDTO exerciseDTO = userCollection.GetExercise("Running");
            UserDTO userDTO = new UserDTO("TempAccountCardioTraining", Guid.NewGuid(), "TempPassword", null, null);
            userCollectionDAL.AddUser(userDTO);
            CardioTrainingDTO cardioTrainingDTO = new CardioTrainingDTO(
                exerciseDTO, 
                5.44M, 
                new TimeSpan(0, 28, 45),
                userDTO.UserID, 
                DateTime.Now, 
                TrainingTypeDTO.Cardio);

            //act
            user.AddCardioTraining(cardioTrainingDTO);

            //assert
            Guid trainingID = dal.GetUserTrainings(userDTO.UserID.ToString())[0].TrainingID;
            CardioTrainingDTO trainingFromDB = dal.GetCardioTraining(trainingID.ToString());

            Assert.AreEqual(userDTO.UserID, trainingFromDB.UserID);
            Assert.AreEqual(cardioTrainingDTO.Date.ToLongDateString(), trainingFromDB.Date.ToLongDateString());
            Assert.AreEqual(TrainingTypeDTO.Cardio, trainingFromDB.TrainingType);
        }
    }
}
