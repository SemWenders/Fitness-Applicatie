using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FitTracker.LogicFactory;
using FitTracker.Interface.DTOs;
using FitTracker.Interface.Interfaces;
using FitTracker.LogicInterface;

namespace FitTracker.UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void AddUser()
        {
            UserDTO userDTO = new UserDTO("NewUser", Guid.NewGuid(), "password", null, null);
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            userCollection.AddUser(userDTO);
        }

        [TestMethod]
        public void GetUser()
        {
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            UserDTO userDTO = userCollection.GetUser("TestAccount");
            Assert.AreEqual(userDTO.UserID, Guid.Parse("94E1E099-538F-4E9E-830A-04952A2DD682"));
        }

        [TestMethod]
        public void GetAllUser()
        {
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            List<UserDTO> userDTOs = userCollection.GetAllUsers();
        }

        [TestMethod]
        public void DoesUserExist()
        {
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            bool exists = userCollection.DoesUserExist("TestAccount");
            Assert.AreEqual(exists, true);
        }
    }
}
