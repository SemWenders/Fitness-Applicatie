using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using FitTracker.Factory;

namespace FitTracker.Logic
{
    public class User
    {
        //properties
        public string Name { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Exercise> Exercises { get; set; }

        //methods
        public void AddExercise(Exercise exercise)
        {
            IExerciseDAL dal = ExerciseFactory.GetExerciseDAL();
            ExerciseDTO exerciseDTO = ConvertExercise(exercise);
            dal.AddExercise(exerciseDTO);
        }

        public void AddWeightTraining(WeightTraining training)
        {
            ITrainingDAL dal = TrainingFactory.GetTrainingDAL();
            WeightTrainingDTO trainingDTO = ConvertWeightTraining(training);
            dal.AddWeightTraining(trainingDTO);
        }

        public void AddCardioTraining(CardioTraining cardioTraining)
        {
            ITrainingDAL dal = TrainingFactory.GetTrainingDAL();
            CardioTrainingDTO cardioTrainingDTO = ConvertCardioTraining(cardioTraining);
            dal.AddCardioTraining(cardioTrainingDTO);
        }

        public void DeleteWeightTraining(WeightTraining weightTraining)
        {
            ITrainingDAL dal = TrainingFactory.GetTrainingDAL();
            dal.DeleteWeightTraining(weightTraining.TrainingID.ToString());
        }

        public List<TrainingDTO> GetUserTrainings()
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            List<TrainingDTO> trainings = trainingDAL.GetUserTrainings(UserID.ToString());
            return trainings;
        }

        public WeightTraining GetWeightTraining(string trainingID)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            WeightTrainingDTO trainingDTO = trainingDAL.GetWeightTraining(trainingID);
            WeightTraining training = ConvertWeightTrainingDTO(trainingDTO); 
            return training;
        }

        private ExerciseDTO ConvertExercise(Exercise exercise)
        {
            ExerciseDTO exerciseDTO = new ExerciseDTO()
            {
                ExerciseID = exercise.ExerciseID,
                ExerciseType = (ExerciseTypeDTO)exercise.ExerciseType,
                Name = exercise.Name,
                UserID = exercise.UserID
            };
            return exerciseDTO;
        }
        private Exercise ConvertExerciseDTO(ExerciseDTO exerciseDTO)
        {
            Exercise exercise = new Exercise()
            {
                ExerciseID = exerciseDTO.ExerciseID,
                ExerciseType = (ExerciseType)exerciseDTO.ExerciseType,
                Name = exerciseDTO.Name,
                UserID = exerciseDTO.UserID
            };
            return exercise;
        }

        private WeightTraining ConvertWeightTrainingDTO(WeightTrainingDTO trainingDTO)
        {
            List<Round> rounds = new List<Round>();
            foreach (var roundDTO in trainingDTO.Rounds)
            {
                rounds.Add(ConvertRoundDTO(roundDTO));
            }
            WeightTraining training = new WeightTraining()
            {
                Date = trainingDTO.Date,
                TrainingID = trainingDTO.TrainingID,
                UserID = trainingDTO.UserID,
                Rounds = rounds
            };
            return training;
        }

        private Round ConvertRoundDTO(RoundDTO roundDTO)
        {
            List<Set> sets = new List<Set>();
            foreach (var setDTO in roundDTO.Sets)
            {
                sets.Add(ConvertSetDTO(setDTO));
            }
            IExerciseDAL dal = ExerciseFactory.GetExerciseDAL();
            Round round = new Round()
            {
                ExerciseID = roundDTO.ExerciseID,
                RoundID = roundDTO.RoundID,
                TrainingID = roundDTO.TrainingID,
                Sets = sets,
                Exercise = ConvertExerciseDTO(dal.GetExerciseDTO(roundDTO.ExerciseID.ToString()))
            };
            return round;
        }
        private Set ConvertSetDTO(SetDTO setDTO)
        {
            throw new NotImplementedException(); //TODO: fill in
        }

        private WeightTrainingDTO ConvertWeightTraining(WeightTraining training)
        {
            List<RoundDTO> roundDTOs = new List<RoundDTO>();
            foreach (var round in training.Rounds)
            {
                roundDTOs.Add(ConvertRound(round));
            }
            WeightTrainingDTO trainingDTO = new WeightTrainingDTO()
            {
                Date = training.Date,
                TrainingID = training.TrainingID,
                UserID = training.UserID,
                Rounds = roundDTOs
            };
            return trainingDTO;
        }

        private RoundDTO ConvertRound(Round round)
        {
            List<SetDTO> setDTOs = new List<SetDTO>();
            foreach (var set in round.Sets)
            {
                setDTOs.Add(ConvertSet(set));
            }
            RoundDTO roundDTO = new RoundDTO()
            {
                ExerciseID = round.ExerciseID,
                RoundID = round.RoundID,
                TrainingID = round.TrainingID,
                Sets = setDTOs
            };
            return roundDTO;
        }

        private SetDTO ConvertSet(Set set)
        {
            SetDTO setDTO = new SetDTO()
            {
                SetID = set.SetID,
                SetOrder = set.SetOrder,
                Weight = set.Weight
            };
            return setDTO;
        }

        private CardioTrainingDTO ConvertCardioTraining(CardioTraining cardioTraining)
        {
            CardioTrainingDTO cardioTrainingDTO = new CardioTrainingDTO()
            {
                Date = cardioTraining.Date,
                Distance = cardioTraining.Distance,
                Exercise = ConvertExercise(cardioTraining.Exercise),
                Time = cardioTraining.Time,
                TrainingID = cardioTraining.TrainingID,
                UserID = cardioTraining.UserID
            };
            return cardioTrainingDTO;
        }
    }
}
