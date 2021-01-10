using System;
using FitTracker.LogicInterface;
using FitTracker.Logic;

namespace LogicFactory
{
    public static class UserFactory
    {
        public static IUser GetUser()
        {
            return new User();
        }
    }
}
