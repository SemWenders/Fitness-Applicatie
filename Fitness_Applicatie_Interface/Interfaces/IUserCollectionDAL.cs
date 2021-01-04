using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.Interface.Interfaces
{
    public interface IUserCollectionDAL
    {
        public void AddUser(UserDTO userDTO);
        public void DeleteUser(string userID);
        public UserDTO GetUser(string userID);
        public List<UserDTO> GetAllUsers();
    }
}
