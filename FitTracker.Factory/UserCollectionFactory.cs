using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Persistence;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Factory
{
    public static class UserCollectionFactory
    {
        public static IUserCollectionDAL GetUserCollectionDAL()
        {
            return new UserCollectionDAL();
        }
    }
}
