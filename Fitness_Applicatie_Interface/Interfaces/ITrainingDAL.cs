using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.Interface.Interfaces
{
    public interface ITrainingDAL
    {
        public void AddTraining(TrainingDTO trainingDTO);
        public void DeleteTraining();
    }
}
