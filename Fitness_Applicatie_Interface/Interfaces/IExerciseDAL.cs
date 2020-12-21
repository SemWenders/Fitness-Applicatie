using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.Interface.Interfaces
{
    public interface IExerciseDAL
    {
        void AddExercise(ExerciseDTO exercise);
        public ExerciseDTO GetExerciseDTO(string exerciseID);
        public List<ExerciseDTO> GetExerciseDTOs();
        void DeleteExercise(string exerciseID);
    }
}
