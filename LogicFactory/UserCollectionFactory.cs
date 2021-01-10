using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.LogicInterface;
using FitTracker.Logic;

namespace FitTracker.LogicFactory
{
    public static class UserCollectionFactory
    {
        public static IUserCollection GetUserCollection()
        {
            return new UserCollection();
        }
    }
}
