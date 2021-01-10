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
    class UserDAL : IUserDAL
    {
        //string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build().GetConnectionString("DefaultConnection");
        }
        public void AddUser(UserDTO user)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@UserID, @Password, @Name)", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Name", user.Name);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID = @UserID", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public UserDTO GetUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserID = @UserID", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                string name = null;
                string password = null;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        name = reader["Name"].ToString();
                        password = reader["Password"].ToString();
                    }
                    UserDTO userDTO = new UserDTO(name, Guid.Parse(userID), password, null, null);
                    return userDTO;
                }
            }
        }
    }
}
