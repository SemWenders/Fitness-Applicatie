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
        public List<User> Users { get; private set; }
        void AddUser()
        {

        }

        void DeleteUser()
        {

        }

        public User GetUser(string username)
        {
            IUserCollectionDAL dal = UserCollectionFactory.GetUserCollectionDAL();
            UserDTO userDTO = dal.GetUser(username);
            User user = ConvertUserDTO(userDTO);
            return user;
        }


        public void DeleteExercise(Exercise exercise)
        {

        }
        private User ConvertUserDTO(UserDTO userDTO)
        {
            User user = new User()
            {
                Name = userDTO.Name,
                UserID = userDTO.UserID,
                Password = userDTO.Password,
                Trainings = ConvertTrainingDTOs(userDTO.Trainings)
            };

            return user;
        }

        private List<Training> ConvertTrainingDTOs(List<TrainingDTO> trainingDTOs)
        {
            List<Training> trainings = new List<Training>();
            foreach (var trainingDTO in trainingDTOs)
            {
                Training training = new Training
                {
                    Date = trainingDTO.Date,
                    TrainingID = trainingDTO.TrainingID,
                    UserID = trainingDTO.UserID
                };
                trainings.Add(training);
            }
            return trainings;
        }
    }
}
