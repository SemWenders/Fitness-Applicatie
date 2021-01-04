using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class TrainingDTO
    {
        public Guid TrainingID { get; private set; }
        public Guid UserID { get; private set; }
        public DateTime Date { get; private set; }
        public TrainingTypeDTO TrainingType { get; private set; }

        public TrainingDTO(Guid trainingID, Guid userID, DateTime date, TrainingTypeDTO trainingTypeDTO)
        {
            TrainingID = trainingID;
            UserID = userID;
            Date = date;
            TrainingType = trainingTypeDTO;
        }

        public TrainingDTO()
        {

        }
    }
}
