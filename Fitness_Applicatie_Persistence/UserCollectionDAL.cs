using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Interface.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FitTracker.Persistence
{
    public class UserCollectionDAL : IUserCollectionDAL
    {
        //string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build().GetConnectionString("DefaultConnection");
        }
        public void AddUser(UserDTO userDTO)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@UserID, @Password, @Name)", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userDTO.UserID);
                cmd.Parameters.AddWithValue("@Password", userDTO.Password);
                cmd.Parameters.AddWithValue("@Name", userDTO.Name);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUser(string userID)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                List<UserDTO> userDTOs = new List<UserDTO>();
                connection.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        Guid userID = Guid.Parse(reader["UserID"].ToString());
                        string password = reader["Password"].ToString();
                        UserDTO userDTO = new UserDTO(name, userID, password, null, null);
                        userDTOs.Add(userDTO);
                    }
                    return userDTOs;
                }
            }
        }

        public UserDTO GetUser(string username)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmdUser = new SqlCommand("SELECT * FROM Users WHERE Name = @Name", connection);
                cmdUser.CommandType = System.Data.CommandType.StoredProcedure;
                cmdUser.Parameters.AddWithValue("@Name", username);
                Guid userID = Guid.Empty;
                string password = null;
                connection.Open();
                using (SqlDataReader reader = cmdUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userID = Guid.Parse(reader["UserID"].ToString());
                        password = reader["Password"].ToString();
                    }
                }

                List<TrainingDTO> trainingDTOs = new List<TrainingDTO>();
                SqlCommand cmdTrainings = new SqlCommand("SELECT * FROM Trainings WHERE UserID = @UserID", connection);
                cmdTrainings.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTrainings.Parameters.AddWithValue("@UserID", username);

                using (SqlDataReader reader = cmdTrainings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid trainingID = Guid.Parse(reader["ID"].ToString());
                        DateTime date = (DateTime)reader["Date"];
                        TrainingTypeDTO trainingType = (TrainingTypeDTO)Enum.Parse(typeof(TrainingTypeDTO), reader["TrainingType"].ToString());
                        TrainingDTO training = new TrainingDTO(trainingID, Guid.Parse(username), date, trainingType);
                        trainingDTOs.Add(training);
                    }
                }
                UserDTO userDTO = new UserDTO(username, userID, password, trainingDTOs, null);
                return userDTO;
            }
        }
    }
}
