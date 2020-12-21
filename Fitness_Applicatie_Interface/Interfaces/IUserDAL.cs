using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.Interface.Interfaces
{
    public interface IUserDAL
    {
        public void AddUser(UserDTO user);
        public UserDTO GetUser(string userID);
        public void DeleteUser(string userID);
    }
}
