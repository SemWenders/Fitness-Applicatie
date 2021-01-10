using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Applicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTracker.Logic;
using FitTracker.Interface.DTOs;

namespace Fitness_Applicatie.Controllers
{
    public class TrainingController : Controller
    {
        [Authorize]
        public IActionResult AddTraining()
        {
            return View();
        }
        [Authorize]
        public IActionResult TrainingDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStrengthTraining(TrainingViewModel trainingViewModel)
        {
            List<RoundDTO> rounds = new List<RoundDTO>();
            foreach (var roundViewModel in trainingViewModel.Rounds)
            {
                rounds.Add(ConvertRoundVM(roundViewModel));
            }
            WeightTrainingDTO weightTraining = new WeightTrainingDTO(rounds, Guid.NewGuid(), Guid.Parse(User.FindFirst("Id").Value), DateTime.Now, TrainingTypeDTO.Strength);
            UserCollection userCollection = new UserCollection();
            User user = ConvertUserDTO(userCollection.GetUser(User.Identity.Name));
            user.AddStrengthTraining(weightTraining);
            return View();
        }

        private RoundDTO ConvertRoundVM(RoundViewModel roundViewModel)
        {
            List<SetDTO> sets = new List<SetDTO>();
            foreach (var set in roundViewModel.Sets)
            {
                sets.Add(ConvertSetVM(set));
            }
            UserCollection userCollection = new UserCollection();
            ExerciseDTO exercise = userCollection.GetExercise(roundViewModel.Exercise.Name);
            RoundDTO round = new RoundDTO(exercise, roundViewModel.RoundID, roundViewModel.TrainingID, roundViewModel.ExerciseID, sets);
            return round;
        }

        private SetDTO ConvertSetVM(SetViewModel setViewModel)
        {
            SetDTO set = new SetDTO(setViewModel.Weight, setViewModel.SetID, setViewModel.SetOrder);
            return set;
        }

        private Exercise ConvertExerciseVM(ExerciseViewModel exerciseViewModel)
        {
            Exercise exercise = new Exercise(exerciseViewModel.ExerciseID, exerciseViewModel.Name, exerciseViewModel.UserID, exerciseViewModel.ExerciseType);
            return exercise;
        }

        private Exercise ConvertExerciseDTO(ExerciseDTO exerciseDTO)
        {
            Exercise exercise = new Exercise(exerciseDTO.ExerciseID, exerciseDTO.Name, exerciseDTO.UserID, (ExerciseType)exerciseDTO.ExerciseType);
            return exercise;
        }

        private User ConvertUserDTO(UserDTO userDTO)
        {
            User user = new User(userDTO.Name, userDTO.UserID, userDTO.Password, ConvertTrainingDTOs(userDTO.GetTrainings()), ConvertExerciseDTOs(userDTO.GetExercises()));

            return user;
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
    }
}
