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
        public string Name { get; private set; }
        public string UserID { get; set; }
        public string Password { get; private set; }
        public List<Training> Trainings { get; private set; }
        public List<Exercise> Exercises { get; private set; }

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

        public TrainingDTO GetTraining(string trainingID)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            TrainingDTO training = trainingDAL.GetWeightTraining(trainingID);
            return training;
        }

        ExerciseDTO ConvertExercise(Exercise exercise)
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

        WeightTrainingDTO ConvertWeightTraining(WeightTraining training)
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

        RoundDTO ConvertRound(Round round)
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

        SetDTO ConvertSet(Set set)
        {
            SetDTO setDTO = new SetDTO()
            {
                SetID = set.SetID,
                SetOrder = set.SetOrder,
                Weight = set.Weight
            };
            return setDTO;
        }

        CardioTrainingDTO ConvertCardioTraining(CardioTraining cardioTraining)
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
