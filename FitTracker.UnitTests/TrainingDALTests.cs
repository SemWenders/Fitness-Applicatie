using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using FitTracker.Factory;
using FitTracker.Logic;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class TrainingDALTests
    {
        [TestMethod]
        public void AddWeightTraining()
        {
            ITrainingDAL trainingDal = TrainingFactory.GetTrainingDAL();
            IExerciseDAL exerciseDAL = ExerciseFactory.GetExerciseDAL();
            List<Round> rounds = new List<Round>
            {
                new Round
                {
                    RoundID = Guid.NewGuid(),
                    ExerciseID = trainingDal.GetExerciseID("Deadlift"),
                    Sets = new List<Set>
                    {
                        new Set
                        {
                            SetID = Guid.NewGuid(),
                            SetOrder = 0,
                            Weight = 80
                        },

                        new Set
                        {
                            SetID = Guid.NewGuid(),
                            SetOrder = 1,
                            Weight = 82.5
                        },

                        new Set
                        {
                            SetID = Guid.NewGuid(),
                            SetOrder = 2,
                            Weight = 85
                        }
                    }
                },

                new Round
                {
                    RoundID = Guid.NewGuid(),
                    ExerciseID = trainingDal.GetExerciseID("Squat"),
                    Sets = new List<Set>
                    {
                        new Set
                        {
                            SetID = Guid.NewGuid(),
                            SetOrder = 0,
                            Weight = 55
                        },

                        new Set
                        {
                            SetID = Guid.NewGuid(),
                            SetOrder = 1,
                            Weight = 57.5
                        },

                        new Set
                        {
                            SetID = Guid.NewGuid(),
                            SetOrder = 2,
                            Weight = 60
                        }
                    }
                }
            };
            WeightTraining weightTraining = new WeightTraining()
            {
                Date = DateTime.Parse("22-12-2020"),
                TrainingID = Guid.NewGuid(),
                UserID = "TestAccount",
                Rounds = rounds
            };
            User user = new User();
            user.AddWeightTraining(weightTraining);
        }
    }
}
