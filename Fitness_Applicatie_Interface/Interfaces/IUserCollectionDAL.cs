using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.Interface.DTOs;

namespace FitTracker.Interface.Interfaces
{
    public interface IUserCollectionDAL
    {
        public void AddUser(UserDTO userDTO);
        public void DeleteUser(string username);
        public UserDTO GetUser(string username);
        public List<UserDTO> GetAllUsers();
        public bool DoesUserExist(string username);
    }
}
