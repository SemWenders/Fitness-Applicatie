using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class TrainingDTO
    {
        public string TrainingID { get; private set; }
        public string UserID { get; private set; }
        public DateTime Date { get; private set; }
        public List<ExerciseDTO> Exercises { get; private set; }
        public List<SetDTO> Sets { get; private set; } //list of weight of each set

        //constructor
        public TrainingDTO(string trainingID, string userID, DateTime date, List<ExerciseDTO> exercises, List<SetDTO> sets)
        {
            this.TrainingID = trainingID;
            this.UserID = userID;
            this.Date = date;
            this.Exercises = exercises;
            this.Sets = sets;
        }
    }
}
