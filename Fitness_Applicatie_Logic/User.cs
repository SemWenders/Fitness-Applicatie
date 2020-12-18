using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using FitTracker.Factory;

namespace FitTracker.Logic
{
    public class User
    {
        //properties
        public string Name { get; private set; }
        public Guid UserID { get; set; }
        public string Password { get; private set; }
        public List<Training> Trainings { get; private set; }
        public List<Exercise> Exercises { get; private set; }

        //methods
        public void AddExercise(Exercise exercise)
        {

        }

        public void AddWeightTraining(WeightTraining training)
        {

        }

        public void AddCardioTraining(CardioTraining cardioTraining)
        {

        }

        public void DeleteExercise(Exercise exercise)
        {

        }

        void DeleteTraining(Training training)
        {

        }

        List<TrainingDTO> GetUserTrainings()
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            List<TrainingDTO> trainings = trainingDAL.GetUserTrainings(UserID.ToString());
            return trainings;
        }

        TrainingDTO GetTraining(string trainingID)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            TrainingDTO training = trainingDAL.GetWeightTraining(trainingID);
            return training;
        }

        //TODO: converter method
    }
}
