using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitTracker.Logic;

namespace Fitness_Applicatie.Models
{
    public class TrainingViewModel
    {
        public Guid TrainingID { get; set; }
        public Exercise Exercise { get; set; }
        public List<Round> Rounds { get; set; }
        public DateTime Date { get; set; }
        public decimal Distance { get; set; }
        public TimeSpan Time { get; set; }
        public TrainingType TrainingType { get; set; }
    }
}
