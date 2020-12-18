using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class RoundDTO
    {
        public ExerciseDTO Exercise { get; set; }
        public List<SetDTO> Sets { get; set; }
        public Guid RoundID { get; set; }
        public Guid TrainingID { get; set; }
        public Guid ExerciseID { get; set; }
    }
}
