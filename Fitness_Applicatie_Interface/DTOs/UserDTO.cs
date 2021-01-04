using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public List<TrainingDTO> Trainings { get; set; }
        public List<ExerciseDTO> Exercises { get; set; }

        public UserDTO()
        {
            Trainings = new List<TrainingDTO>();
        }
    }
}
