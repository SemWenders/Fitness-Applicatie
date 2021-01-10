using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.LogicInterface
{
    public interface IUserCollection
    {
        public void AddUser(UserDTO user);
        void DeleteUser();
        public UserDTO GetUser(string username);
        public ExerciseDTO GetExercise(string exerciseName);
        public List<UserDTO> GetAllUsers();
        public void DeleteExercise(ExerciseDTO exercise);

    }
}
