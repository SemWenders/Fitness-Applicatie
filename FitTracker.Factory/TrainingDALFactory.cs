﻿using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Persistence;

namespace FitTracker.Factory
{
    public static class TrainingDALFactory
    {
        public static ITrainingDAL GetTrainingDAL()
        {
            return new TrainingDAL();
        }
    }
}
