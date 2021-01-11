using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fitness_Applicatie.Models;
using FitTracker.Logic;
using FitTracker.Interface.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Fitness_Applicatie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            UserCollection userCollection = new UserCollection();
            User user = ConvertUserDTO(userCollection.GetUser(User.FindFirst("Id").Value));
            UserViewModel userViewModel = new UserViewModel
            {
                Name = user.Name,
                Trainings = user.GetTrainings(),
                UserID = user.UserID.ToString()
            };
            return View(userViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TrainingDetail(string id)
        {
            User user = new User(null, Guid.Empty, null, null, null);
            TrainingViewModel trainingViewModel = new TrainingViewModel();
            Training training = ConvertTrainingDTO(user.GetTraining(id));

            if (training.TrainingType == TrainingType.Strength)
            {
                WeightTraining weightTraining = ConvertWeightTrainingDTO(user.GetWeightTraining(id));
                List<RoundViewModel> roundViewModels = new List<RoundViewModel>();
                foreach (var round in weightTraining.GetRounds())
                {
                    roundViewModels.Add(ConvertRoundVM(round));
                }
                trainingViewModel.Rounds = roundViewModels;
                trainingViewModel.Date = weightTraining.Date;
                trainingViewModel.TrainingID = weightTraining.TrainingID;
                trainingViewModel.TrainingType = weightTraining.TrainingType;
            }
            else if (training.TrainingType == TrainingType.Cardio)
            {
                CardioTraining cardioTraining = ConvertCardioTrainingDTO(user.GetCardioTraining(id));
                trainingViewModel.Exercise = cardioTraining.Exercise;
                trainingViewModel.Distance = cardioTraining.Distance;
                trainingViewModel.Time = cardioTraining.Time;
                trainingViewModel.TrainingID = cardioTraining.TrainingID;
                trainingViewModel.TrainingType = cardioTraining.TrainingType;
            }
            return View("../Training/TrainingDetail", trainingViewModel);
        }

        private TrainingViewModel ConvertWeightTrainingVM(WeightTraining weightTraining)
        {
            List<RoundViewModel> roundViewModels = new List<RoundViewModel>();
            foreach (var round in weightTraining.GetRounds())
            {
                roundViewModels.Add(ConvertRoundVM(round));
            }
            TrainingViewModel trainingViewModel = new TrainingViewModel
            {
                Date = weightTraining.Date,
                Rounds = roundViewModels,
                TrainingID = weightTraining.TrainingID,
                TrainingType = weightTraining.TrainingType
            };
            return trainingViewModel;
        }

        private RoundViewModel ConvertRoundVM(Round round)
        {
            List<SetViewModel> setViewModels = new List<SetViewModel>();
            foreach (var set in round.GetSets())
            {
                setViewModels.Add(ConvertSetVM(set));
            }
            RoundViewModel roundViewModel = new RoundViewModel();
            return roundViewModel;
        }

        SetViewModel ConvertSetVM(Set set)
        {
            SetViewModel setViewModel = new SetViewModel
            {
                Weight = set.Weight,
                SetID = set.SetID,
                SetOrder = set.SetOrder
            };
            return setViewModel;
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
        private List<ExerciseDTO> ConvertExercises(List<Exercise> exercises)
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

        private Training ConvertTrainingDTO(TrainingDTO trainingDTO)
        {
            Training training = new Training(trainingDTO.TrainingID, trainingDTO.UserID, trainingDTO.Date, (TrainingType)trainingDTO.TrainingType);
            return training;
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

        private WeightTraining ConvertWeightTrainingDTO(WeightTrainingDTO trainingDTO)
        {
            List<Round> rounds = new List<Round>();
            foreach (var roundDTO in trainingDTO.GetRounds())
            {
                rounds.Add(ConvertRoundDTO(roundDTO));
            }
            WeightTraining training = new WeightTraining(rounds, trainingDTO.TrainingID, trainingDTO.UserID, trainingDTO.Date, (TrainingType)trainingDTO.TrainingType);
            return training;
        }

        private Round ConvertRoundDTO(RoundDTO roundDTO)
        {
            List<Set> sets = new List<Set>();
            foreach (var setDTO in roundDTO.GetSets())
            {
                sets.Add(ConvertSetDTO(setDTO));
            }
            User user = new User();
            Round round = new Round(
                ConvertExerciseDTO(user.GetExercise(roundDTO.ExerciseID.ToString())),
                sets,
                roundDTO.RoundID,
                roundDTO.TrainingID,
                roundDTO.ExerciseID
                );
            return round;
        }
        private Set ConvertSetDTO(SetDTO setDTO)
        {
            Set set = new Set(setDTO.Weight, setDTO.SetOrder, setDTO.SetID, setDTO.RoundID);
            return set;
        }

        private ExerciseDTO ConvertExercise(Exercise exercise)
        {
            ExerciseDTO exerciseDTO = new ExerciseDTO(exercise.ExerciseID, exercise.Name, exercise.UserID, (ExerciseTypeDTO)exercise.ExerciseType);
            return exerciseDTO;
        }

        private CardioTrainingDTO ConvertCardioTraining(CardioTraining cardioTraining)
        {
            CardioTrainingDTO cardioTrainingDTO = new CardioTrainingDTO(
                ConvertExercise(cardioTraining.Exercise),
                cardioTraining.Distance,
                cardioTraining.Time,
                cardioTraining.TrainingID,
                cardioTraining.UserID,
                cardioTraining.Date,
                (TrainingTypeDTO)cardioTraining.TrainingType);
            return cardioTrainingDTO;
        }

        private CardioTraining ConvertCardioTrainingDTO(CardioTrainingDTO cardioTrainingDTO)
        {
            CardioTraining cardioTraining = new CardioTraining(
                ConvertExerciseDTO(cardioTrainingDTO.Exercise),
                cardioTrainingDTO.Distance,
                cardioTrainingDTO.Time,
                cardioTrainingDTO.TrainingID,
                cardioTrainingDTO.UserID,
                cardioTrainingDTO.Date,
                (TrainingType)cardioTrainingDTO.TrainingType
                );
            return cardioTraining;
        }
    }
}
