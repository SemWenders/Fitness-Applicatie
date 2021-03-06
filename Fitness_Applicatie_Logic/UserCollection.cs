﻿using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Factory;
using FitTracker.Interface.DTOs;
using FitTracker.LogicInterface;
using Microsoft.AspNetCore.Identity;

namespace FitTracker.Logic
{
    public class UserCollection : IUserCollection
    {
        private List<User> users;

        public UserCollection()
        {
            //users = GetAllUsers();
        }


        public void AddUser(UserDTO user)
        {
            var hasher = new PasswordHasher<User>();
            User tempUser = new User();
            string hashedPW = hasher.HashPassword(tempUser, user.Password);
            UserDTO hashedPwUser = new UserDTO(user.Name, Guid.NewGuid(), hashedPW, null, null);
            IUserCollectionDAL dal = UserCollectionDALFactory.GetUserCollectionDAL();
            dal.AddUser(hashedPwUser);
        }


        public void DeleteUser(string userID)
        {
            IUserCollectionDAL dal = UserCollectionDALFactory.GetUserCollectionDAL();
            dal.DeleteUser(userID);
        }

        public List<UserDTO> Users()
        {
            return new List<UserDTO>();
        }

        public UserDTO GetUser(string username)
        {
            IUserCollectionDAL dal = UserCollectionDALFactory.GetUserCollectionDAL();
            UserDTO user = dal.GetUser(username);
            return user;
        }

        public ExerciseDTO GetExercise(string exerciseName)
        {
            IExerciseDAL exerciseDAL = ExerciseDALFactory.GetExerciseDAL();
            ExerciseDTO exercise = exerciseDAL.GetExerciseDTOByName(exerciseName);
            return exercise;
        }

        public List<UserDTO> GetAllUsers()
        {
            IUserCollectionDAL dal = UserCollectionDALFactory.GetUserCollectionDAL();
            List<UserDTO> users = dal.GetAllUsers();
            return users;
        }

        public void DeleteExercise(string exerciseID)
        {
            IExerciseDAL dal = ExerciseDALFactory.GetExerciseDAL();
            dal.DeleteExercise(exerciseID);
        }

        public bool DoesUserExist(string username)
        {
            IUserCollectionDAL dal = UserCollectionDALFactory.GetUserCollectionDAL();
            return dal.DoesUserExist(username);
        }
        private UserDTO ConvertUser(User user)
        {
            UserDTO userDTO = new UserDTO(user.Name, user.UserID, user.Password, ConvertTraining(user.GetTrainings()), ConvertExercise(user.GetExercises()));
            return userDTO;
        }

        private User ConvertUserDTO(UserDTO userDTO)
        {
            User user = new User(userDTO.Name, userDTO.UserID, userDTO.Password, ConvertTrainingDTOs(userDTO.GetTrainings()), ConvertExerciseDTOs(userDTO.GetExercises()));

            return user;
        }

        private Exercise ConvertExerciseDTO(ExerciseDTO exerciseDTO)
        {
            Exercise exercise = new Exercise(exerciseDTO.ExerciseID, exerciseDTO.Name, exerciseDTO.UserID, (ExerciseType)exerciseDTO.ExerciseType);
            return exercise;
        }
        private List<ExerciseDTO> ConvertExercise(List<Exercise> exercises)
        {
            List<ExerciseDTO> exerciseDTOs = new List<ExerciseDTO>();
            if (exercises != null)
            {
                foreach (var exercise in exercises)
                {
                    ExerciseDTO exerciseDTO = new ExerciseDTO(exercise.ExerciseID, exercise.Name, exercise.UserID, (ExerciseTypeDTO)exercise.ExerciseType);
                    exerciseDTOs.Add(exerciseDTO);
                }
            }
            return exerciseDTOs;
        }

        private List<Exercise> ConvertExerciseDTOs(List<ExerciseDTO> exerciseDTOs)
        {
            List<Exercise> exercises = new List<Exercise>();
            if (exerciseDTOs != null)
            {
                foreach (var exerciseDTO in exerciseDTOs)
                {
                    Exercise exercise = new Exercise(exerciseDTO.ExerciseID, exerciseDTO.Name, exerciseDTO.UserID, (ExerciseType)exerciseDTO.ExerciseType);
                    exercises.Add(exercise);
                }
            }
            
            return exercises;
        }

        private List<TrainingDTO> ConvertTraining(List<Training> trainings)
        {
            List<TrainingDTO> trainingDTOs = new List<TrainingDTO>();
            if (trainings != null)
            {
                foreach (var training in trainings)
                {
                    TrainingDTO trainingDTO = new TrainingDTO(training.TrainingID, training.UserID, training.Date, (TrainingTypeDTO)training.TrainingType);
                    trainingDTOs.Add(trainingDTO);
                }
            }
            
            return trainingDTOs;
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
