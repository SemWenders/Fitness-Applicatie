using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Factory;
using FitTracker.Interface.DTOs;

namespace FitTracker.Logic
{
    public class UserCollection
    {
        private List<User> users;

        public UserCollection()
        {
            //users = GetAllUsers();
        }


        void AddUser()
        {

        }

        void DeleteUser()
        {

        }

        public List<User> Users()
        {
            return users;
        }

        public User GetUser(string username)
        {
            IUserCollectionDAL dal = UserCollectionFactory.GetUserCollectionDAL();
            UserDTO userDTO = dal.GetUser(username);
            User user = ConvertUserDTO(userDTO);
            return user;
        }

        private List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            IUserCollectionDAL dal = UserCollectionFactory.GetUserCollectionDAL();
            List<UserDTO> userDTOs = dal.GetAllUsers();
            foreach (var userdto in userDTOs)
            {
                User user = ConvertUserDTO(userdto);
                users.Add(user);
            }
            return users;
        }

        public void DeleteExercise(Exercise exercise)
        {

        }
        private User ConvertUserDTO(UserDTO userDTO)
        {
            User user = new User(userDTO.Name, userDTO.UserID, userDTO.Password, ConvertTrainingDTOs(userDTO.GetTrainings()), ConvertExerciseDTOs(userDTO.GetExercises()));

            return user;
        }

        private List<Exercise> ConvertExerciseDTOs(List<ExerciseDTO> exerciseDTOs)
        {
            List<Exercise> exercises = new List<Exercise>();
            foreach (var exerciseDTO in exerciseDTOs)
            {
                Exercise exercise = new Exercise(exerciseDTO.ExerciseID, exerciseDTO.Name, exerciseDTO.UserID, (ExerciseType)exerciseDTO.ExerciseType);
                exercises.Add(exercise);
            }
            return exercises;
        }

        private List<Training> ConvertTrainingDTOs(List<TrainingDTO> trainingDTOs)
        {
            List<Training> trainings = new List<Training>();
            foreach (var trainingDTO in trainingDTOs)
            {
                Training training = new Training(trainingDTO.TrainingID, trainingDTO.UserID, trainingDTO.Date, (TrainingType)trainingDTO.TrainingType);
                trainings.Add(training);
            }
            return trainings;
        }
    }
}
