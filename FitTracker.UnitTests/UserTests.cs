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
        [ClassCleanup]
        public static void CleanTests()
        {
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            userCollection.DeleteUser("AddUserTest");
            userCollection.DeleteUser("GetUserTest");
            userCollection.DeleteUser("GetAllUsersTest");
            userCollection.DeleteUser("DoesUserExistTest");
        }

        [TestMethod]
        public void AddUser()
        {
            //arrange
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            UserDTO userDTO = new UserDTO("AddUserTest", "AddUserTestPassword");

            //act
            userCollection.AddUser(userDTO);

            //assert
            UserDTO userFromDB = userCollection.GetUser(userDTO.Name);

            Assert.AreEqual(userDTO.Name, userFromDB.Name);
        }

        [TestMethod]
        public void GetUser()
        {
            //arrange
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            UserDTO userDTO = new UserDTO("GetUserTest", "AddUserTestPassword");
            userCollection.AddUser(userDTO);

            //act
            UserDTO userFromDB = userCollection.GetUser("GetUserTest");

            //assert

            Assert.AreEqual(userDTO.Name, userFromDB.Name);
        }

        [TestMethod]
        public void GetAllUser()
        {
            //arrange
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            UserDTO userDTO = new UserDTO("GetAllUsersTest", "AddUserTestPassword");
            userCollection.AddUser(userDTO);

            //act
            List<UserDTO> userDTOs = userCollection.GetAllUsers();

            //assertt
            List<string> names = new List<string>();
            foreach (var user in userDTOs)
            {
                names.Add(user.Name);
            }
            bool contains = names.Contains(userDTO.Name);
            Assert.AreEqual(true, contains);
        }

        [TestMethod]
        public void DoesUserExist()
        {
            //arrange
            IUserCollection userCollection = UserCollectionFactory.GetUserCollection();
            UserDTO userDTO = new UserDTO("DoesUserExistTest", "AddUserTestPassword");
            userCollection.AddUser(userDTO);

            //act
            bool exists = userCollection.DoesUserExist("DoesUserExistTest");

            //assert
            Assert.AreEqual(exists, true);

        }
    }
}
