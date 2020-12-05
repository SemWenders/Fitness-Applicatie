using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    class User
    {
        //properties
        public string Name { get; private set; }
        public string UserID { get; private set; }
        public string Password { get; private set; }
        public List<Training> Trainings { get; private set; }
        public List<Exercise> Exercises { get; private set; }

        //methods
        void AddExercise(Exercise exercise)
        {

        }

        void AddTraining(Training training)
        {

        }

        void DeleteExercise(Exercise exercise)
        {

        }

        void DeleteTraining(Training training)
        {

        }
    }
}
