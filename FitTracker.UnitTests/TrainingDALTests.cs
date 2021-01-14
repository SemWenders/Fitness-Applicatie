using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using FitTracker.LogicFactory;
using LogicFactory;
using FitTracker.LogicInterface;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class TrainingDALTests
    {
        [TestMethod]
        public void AddWeightTraining()
        {
            IUser user = UserFactory.GetUser();
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            Guid trainingID = Guid.NewGuid();
            Guid deadliftRoundID = Guid.NewGuid();
            Guid squatRoundID = Guid.NewGuid();
            Guid pullupRoundID = Guid.NewGuid();
            ExerciseDTO deadlift = userCollection.GetExercise("Deadlift");
            ExerciseDTO squat = userCollection.GetExercise("Squat");
            ExerciseDTO pullup = userCollection.GetExercise("Pullup");
            List<SetDTO> deadliftSets = new List<SetDTO>
            {
                new SetDTO(80, Guid.NewGuid(), 0, deadliftRoundID),
                new SetDTO(85, Guid.NewGuid(), 1, deadliftRoundID),
                new SetDTO(90, Guid.NewGuid(), 2, deadliftRoundID)
            };

            List<SetDTO> squatSets = new List<SetDTO>
            {
                new SetDTO(50, Guid.NewGuid(), 0, squatRoundID),
                new SetDTO(55, Guid.NewGuid(), 1, squatRoundID),
                new SetDTO(60, Guid.NewGuid(), 2, squatRoundID)
            };

            List<SetDTO> pullupSets = new List<SetDTO>
            {
                new SetDTO(7, Guid.NewGuid(), 0, pullupRoundID),
                new SetDTO(7, Guid.NewGuid(), 1, pullupRoundID),
                new SetDTO(7, Guid.NewGuid(), 2, pullupRoundID)
            };

            List<RoundDTO> rounds = new List<RoundDTO>
            {
                new RoundDTO(deadlift, deadliftRoundID, trainingID, deadlift.ExerciseID, deadliftSets),
                new RoundDTO(squat, squatRoundID, trainingID, squat.ExerciseID, squatSets),
                new RoundDTO(pullup, pullupRoundID, trainingID, pullup.ExerciseID, pullupSets)
            };

            WeightTrainingDTO weightTrainingDTO = new WeightTrainingDTO(rounds, trainingID, Guid.Parse("94E1E099-538F-4E9E-830A-04952A2DD682"), DateTime.Now, TrainingTypeDTO.Strength);
            user.AddStrengthTraining(weightTrainingDTO);
        }

        [TestMethod]
        public void AddCardioTraining()
        {
            IUser user = UserFactory.GetUser();
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            ExerciseDTO exerciseDTO = userCollection.GetExercise("Running");
            CardioTrainingDTO cardioTrainingDTO = new CardioTrainingDTO(
                exerciseDTO, 
                5.44M, 
                new TimeSpan(0, 28, 45), 
                Guid.NewGuid(), 
                Guid.Parse("94E1E099-538F-4E9E-830A-04952A2DD682"), 
                DateTime.Now, 
                TrainingTypeDTO.Cardio);

            user.AddCardioTraining(cardioTrainingDTO);
        }
    }
}
