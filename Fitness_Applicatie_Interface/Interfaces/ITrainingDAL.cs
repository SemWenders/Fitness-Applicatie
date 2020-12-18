using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.Interface.Interfaces
{
    public interface ITrainingDAL
    {
        public void AddWeightTraining(WeightTrainingDTO trainingDTO);
        public void AddCardioTraining(CardioTrainingDTO trainingDTO);
        public List<TrainingDTO> GetUserTrainings(string userID);
        public WeightTrainingDTO GetWeightTraining(string trainingID);
        public CardioTrainingDTO GetCardioTraining(string trainingID);
        public void DeleteTraining(string trainingID);
        public ExerciseDTO GetExerciseDTO(string exerciseID);
    }
}
