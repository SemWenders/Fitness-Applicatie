using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class TrainingDTO
    {
        public string trainingID { get; private set; }
        public string UserID { get; private set; }
        public DateTime Date { get; private set; }
        public List<ExerciseDTO> ExerciseNames { get; private set; }
        public List<int> Sets { get; private set; } //list of weight of each set
    }
}
