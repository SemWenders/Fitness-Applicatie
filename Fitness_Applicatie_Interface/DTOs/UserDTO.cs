using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class UserDTO
    {
        public string Name { get; private set; }
        public string UserID { get; private set; }
        public string Password { get; private set; }
        public List<TrainingDTO> Trainings { get; private set; }
        public List<ExerciseDTO> Exercises { get; private set; }

        //constructor
        public UserDTO(string Name, string UserID, string Password, List<TrainingDTO> Trainings, List<ExerciseDTO> Exercises)
        {
            this.Name = Name;
            this.UserID = UserID;
            this.Password = Password;
            this.Trainings = Trainings;
            this.Exercises = Exercises;
        }
    }
}
