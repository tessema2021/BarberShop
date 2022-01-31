using Microsoft.Data.SqlClient;
using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BarberShop.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {

        private readonly IConfiguration _config;
        DateTime dateTimeVariable = DateTime.Now;

        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public UserProfile GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, FirebaseId, FirstName,LastName, CreateDateTime,DisplayName, Email, UserTypeId 
                                    FROM UserProfile
                                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", id);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public UserProfile GetByFirebaseId(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, FirebaseId, FirstName,LastName, CreateDateTime,DisplayName, Email, UserTypeId 
                                    FROM UserProfile
                                    WHERE FirebaseId = @FirebaseId";

                    cmd.Parameters.AddWithValue("@FirebaseId", firebaseId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }




        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO
                                        UserProfile (FirebaseId, FirstName,LastName, CreateDateTime,DisplayName, Email, UserTypeId ) 
                                        OUTPUT INSERTED.ID
                                        VALUES(@firebaseId, @firstName,@lastName, @createDateTime,@displayName, @email, @userTypeId )";

                    cmd.Parameters.AddWithValue("@firebaseId", userProfile.FirebaseId);
                    cmd.Parameters.AddWithValue("@firstName", userProfile.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", userProfile.LastName);
                    cmd.Parameters.AddWithValue("@createDateTime",dateTimeVariable);
                    cmd.Parameters.AddWithValue("@displayName", userProfile.DisplayName);
                    cmd.Parameters.AddWithValue("@email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@userTypeId", 1);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}