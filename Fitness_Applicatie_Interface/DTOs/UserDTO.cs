using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class UserDTO
    {
        public string Name { get; private set; }
        public Guid UserID { get; private set; }
        public string Password { get; private set; }
        private List<TrainingDTO> trainings;
        private List<ExerciseDTO> exercises;

        public UserDTO(string name, Guid userID, string password, List<TrainingDTO> trainings, List<ExerciseDTO> exercises)
        {
            Name = name;
            UserID = userID;
            Password = password;
            this.trainings = trainings;
            this.exercises = exercises;
        }

        public List<TrainingDTO> GetTrainings()
        {
            return trainings;
        }

        public List<ExerciseDTO> GetExercises()
        {
            return exercises;
        }
    }
}
