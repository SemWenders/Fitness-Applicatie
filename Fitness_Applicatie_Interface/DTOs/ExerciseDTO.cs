using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class ExerciseDTO
    {
        public Guid ExerciseID { get; private set; }
        public string Name { get; private set; }
        public Guid UserID { get; private set; }
        public ExerciseType ExerciseType { get; private set; }
    }
}
