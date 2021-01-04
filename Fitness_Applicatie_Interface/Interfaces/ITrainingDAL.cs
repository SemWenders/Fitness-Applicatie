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
        public TrainingDTO GetTraining(string trainingID);
        public WeightTrainingDTO GetWeightTraining(string trainingID);
        public CardioTrainingDTO GetCardioTraining(string trainingID);
        public void DeleteWeightTraining(string trainingID);
        public void DeleteCardioTraining(string trainingID);
        public ExerciseDTO GetExerciseDTO(string exerciseID);
        public Guid GetExerciseID(string exerciseName);
    }
}
