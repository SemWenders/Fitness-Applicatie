using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class ExerciseDTO
    {
        public Guid ExerciseID { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public ExerciseTypeDTO ExerciseType { get; set; }
    }
}
