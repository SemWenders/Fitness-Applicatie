using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.LogicInterface
{
    public interface IUser
    {
        public void AddStrengthTraining(WeightTrainingDTO training);
        public void AddExercise(ExerciseDTO exercise);
        public void AddCardioTraining(CardioTrainingDTO cardioTraining);
        public void DeleteWeightTraining(string trainingID);
        public void DeleteCardioTraining(string trainingID);
        public List<TrainingDTO> GetUserTrainings();
        public WeightTrainingDTO GetWeightTraining(string trainingID);
        public CardioTrainingDTO GetCardioTraining(string trainingID);
        public TrainingDTO GetTraining(string trainingID);
        public ExerciseDTO GetExercise(string exerciseID);
    }
}
