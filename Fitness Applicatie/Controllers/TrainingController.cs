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
        public IActionResult TrainingDetail(Guid id)
        {
            User user = new User();
            TrainingViewModel trainingVM = ConvertTrainingDTO(user.GetTraining(id.ToString()));
            
            if (trainingVM.TrainingType == TrainingType.Strength)
            {
                WeightTrainingDTO weightTrainingDTO = user.GetWeightTraining(id.ToString());
                List<RoundViewModel> roundViewModels = new List<RoundViewModel>();
                foreach (var roundDTO in weightTrainingDTO.GetRounds())
                {
                    roundViewModels.Add(ConvertRoundDTO(roundDTO));
                }
                trainingVM.Rounds = roundViewModels;
            }

            else
            {

            }
            return View(trainingVM);
        }

        [HttpPost]
        public IActionResult AddStrengthTraining(TrainingViewModel trainingViewModel)
        {
            //TODO: set textboxes of sets to empty
            List<RoundDTO> rounds = new List<RoundDTO>();
            Guid trainingID = Guid.NewGuid();
            foreach (var roundViewModel in trainingViewModel.Rounds)
            {
                roundViewModel.TrainingID = trainingID;
            }
            foreach (var roundViewModel in trainingViewModel.Rounds)
            {
                rounds.Add(ConvertRoundVM(roundViewModel));
            }
            WeightTrainingDTO weightTraining = new WeightTrainingDTO(rounds, trainingID, Guid.Parse(User.FindFirst("Id").Value), DateTime.Now, TrainingTypeDTO.Strength);
            UserCollection userCollection = new UserCollection();
            User user = ConvertUserDTO(userCollection.GetUser(User.Identity.Name));
            user.AddStrengthTraining(weightTraining);
            return LocalRedirect("/Home/Index");
        }

        private RoundDTO ConvertRoundVM(RoundViewModel roundViewModel)
        {
            List<SetDTO> sets = new List<SetDTO>();
            Guid roundID = Guid.NewGuid();
            foreach (var set in roundViewModel.Sets)
            {
                set.RoundID = roundID;
            }
            foreach (var set in roundViewModel.Sets)
            {
                sets.Add(ConvertSetVM(set));
            }
            UserCollection userCollection = new UserCollection();
            ExerciseDTO exercise = userCollection.GetExercise(roundViewModel.Exercise.Name);
            RoundDTO round = new RoundDTO(exercise, roundID, roundViewModel.TrainingID, exercise.ExerciseID, sets);
            return round;
        }

        private RoundViewModel ConvertRoundDTO(RoundDTO roundDTO)
        {
            List<SetViewModel> setViewModels = new List<SetViewModel>();
            foreach (var setDTO in roundDTO.GetSets())
            {
                setViewModels.Add(ConvertSetDTO(setDTO));
            };
            RoundViewModel roundViewModel = new RoundViewModel
            {
                Exercise = ConvertExerciseDTOToVM(roundDTO.Exercise),
                ExerciseID = roundDTO.ExerciseID,
                RoundID = roundDTO.RoundID,
                TrainingID = roundDTO.TrainingID,
                Sets = setViewModels
            };
            return roundViewModel;
        }

        private SetDTO ConvertSetVM(SetViewModel setViewModel)
        {
            SetDTO set = new SetDTO(setViewModel.Weight, Guid.NewGuid(), setViewModel.SetOrder, setViewModel.RoundID);
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

        private ExerciseViewModel ConvertExerciseDTOToVM(ExerciseDTO exerciseDTO)
        {
            ExerciseViewModel exerciseViewModel = new ExerciseViewModel
            {
                ExerciseID = exerciseDTO.ExerciseID,
                ExerciseType = (ExerciseType)exerciseDTO.ExerciseType,
                Name = exerciseDTO.Name,
                UserID = exerciseDTO.UserID
            };
            return exerciseViewModel;
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

        private TrainingViewModel ConvertTrainingDTO(TrainingDTO trainingDTO)
        {
            TrainingViewModel training = new TrainingViewModel
            {
                Date = trainingDTO.Date,
                TrainingID = trainingDTO.TrainingID,
                TrainingType = (TrainingType)trainingDTO.TrainingType
            };
            return training;
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

        private SetViewModel ConvertSetDTO(SetDTO setDTO)
        {
            SetViewModel setViewModel = new SetViewModel
            {
                RoundID = setDTO.RoundID,
                SetID = setDTO.SetID,
                SetOrder = setDTO.SetOrder,
                Weight = setDTO.Weight
            };
            return setViewModel;
        }
    }
}
