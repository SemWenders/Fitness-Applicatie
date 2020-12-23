using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class TrainingDTO
    {
        public Guid TrainingID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }
    }
}
