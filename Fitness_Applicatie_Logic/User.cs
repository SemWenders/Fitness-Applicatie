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
        public Guid UserID { get; set; }
        public string Password { get; private set; }
        public List<Training> Trainings { get; private set; }
        public List<Exercise> Exercises { get; private set; }

        //methods
        public void AddExercise(Exercise exercise)
        {
            IExerciseDAL exerciseDAL = ExerciseFactory.GetExerciseDAL();
            exerciseDAL.AddExercise(new ExerciseDTO(exercise.Bodyweight, exercise.Name, exercise.UserID));
        }

        public void AddTraining(Training training)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            TrainingDTO trainingDTO = new TrainingDTO
            {
                Date = training.Date,
                Exercises = training.ExercisesNames.ConvertAll
            };
            trainingDAL.AddTraining(trainingDTO);
        }

        public void DeleteExercise(Exercise exercise)
        {

        }

        void DeleteTraining(Training training)
        {

        }

        List<TrainingDTO> GetUserTrainings()
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            List<TrainingDTO> trainings = trainingDAL.GetUserTrainings(UserID.ToString());
            return trainings;
        }

        TrainingDTO GetTraining(string trainingID)
        {
            ITrainingDAL trainingDAL = TrainingFactory.GetTrainingDAL();
            TrainingDTO training = trainingDAL.GetTraining(trainingID);
            return training;
        }

        //TODO: converter method
    }
}
