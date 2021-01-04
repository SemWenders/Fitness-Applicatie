using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Training
    {
        public Guid TrainingID { get; private set; }
        public Guid UserID { get; private set; }
        public DateTime Date { get; private set; }
        public TrainingType TrainingType { get; private set; }

        public Training(Guid trainingID, Guid userID, DateTime date, TrainingType trainingType)
        {
            TrainingID = trainingID;
            UserID = userID;
            Date = date;
            TrainingType = trainingType;
        }
    }
}
