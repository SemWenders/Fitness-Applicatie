﻿using System;
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
        public Guid UserID { get; private set; }
        public string Password { get; private set; }
        private List<Training> trainings;
        private List<Exercise> exercises;

        public List<Training> GetTrainings()
        {
            return trainings;
        }

        public List<Exercise> GetExercises()
        {
            return exercises;
        }

        //constructor
        public User(string name, Guid userID, string password, List<Training> trainings, List<Exercise> exercises)
        {
            Name = name;
            UserID = userID;
            Password = password;
            this.trainings = trainings;
            this.exercises = exercises;
        }

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

        public CardioTraining GetCardioTraining(string trainingID)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            CardioTrainingDTO cardioTrainingDTO = trainingDAL.GetCardioTraining(trainingID);
            CardioTraining cardioTraining = ConvertCardioTrainingDTO(cardioTrainingDTO);
            return cardioTraining;
        }


        public Training GetTraining(string trainingID)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            TrainingDTO trainingDTO = trainingDAL.GetTraining(trainingID);
            Training training = ConvertTrainingDTO(trainingDTO);
            return training;
        }

        private Training ConvertTrainingDTO(TrainingDTO trainingDTO)
        {
            Training training = new Training(trainingDTO.TrainingID, trainingDTO.UserID, trainingDTO.Date, (TrainingType)trainingDTO.TrainingType);

            return training;
        }

        private CardioTraining ConvertCardioTrainingDTO(CardioTrainingDTO cardioTrainingDTO)
        {
            CardioTraining cardioTraining = new CardioTraining(ConvertExerciseDTO(cardioTrainingDTO.Exercise), cardioTrainingDTO.Distance, cardioTrainingDTO.Time, cardioTrainingDTO.TrainingID, cardioTrainingDTO.UserID, cardioTrainingDTO.Date, (TrainingType)cardioTrainingDTO.TrainingType);
            return cardioTraining;
        }

        private ExerciseDTO ConvertExercise(Exercise exercise)
        {
            ExerciseDTO exerciseDTO = new ExerciseDTO(exercise.ExerciseID, exercise.Name, exercise.UserID, (ExerciseTypeDTO)exercise.ExerciseType);
            return exerciseDTO;
        }
        private Exercise ConvertExerciseDTO(ExerciseDTO exerciseDTO)
        {
            Exercise exercise = new Exercise(exerciseDTO.ExerciseID, exerciseDTO.Name, exerciseDTO.UserID, (ExerciseType)exerciseDTO.ExerciseType);
            return exercise;
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
            IExerciseDAL dal = ExerciseFactory.GetExerciseDAL();
            Round round = new Round(
                ConvertExerciseDTO(dal.GetExerciseDTO(roundDTO.ExerciseID.ToString())),
                sets,
                roundDTO.RoundID,
                roundDTO.TrainingID,
                roundDTO.ExerciseID
                );
            return round;
        }
        private Set ConvertSetDTO(SetDTO setDTO)
        {
            throw new NotImplementedException(); //TODO: fill in
        }

        private WeightTrainingDTO ConvertWeightTraining(WeightTraining training)
        {
            List<RoundDTO> roundDTOs = new List<RoundDTO>();
            foreach (var round in training.GetRounds())
            {
                roundDTOs.Add(ConvertRound(round));
            }
            WeightTrainingDTO trainingDTO = new WeightTrainingDTO(roundDTOs, training.TrainingID, training.UserID, training.Date, (TrainingTypeDTO)training.TrainingType);
            return trainingDTO;
        }

        private RoundDTO ConvertRound(Round round)
        {
            List<SetDTO> setDTOs = new List<SetDTO>();
            foreach (var set in round.GetSets())
            {
                setDTOs.Add(ConvertSet(set));
            }
            IExerciseDAL dal = ExerciseFactory.GetExerciseDAL();
            RoundDTO roundDTO = new RoundDTO(
                dal.GetExerciseDTO(round.ExerciseID.ToString()),
                round.RoundID,
                round.TrainingID,
                round.ExerciseID,
                setDTOs);
            return roundDTO;
        }

        private SetDTO ConvertSet(Set set)
        {
            SetDTO setDTO = new SetDTO(set.Weight, set.SetID, set.SetOrder);
            return setDTO;
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
    }
}
