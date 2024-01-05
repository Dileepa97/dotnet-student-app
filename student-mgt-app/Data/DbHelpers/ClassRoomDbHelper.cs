using Microsoft.Extensions.Configuration;
using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public class ClassRoomDbHelper : IClassRoomDbHelper
    {
        private readonly string connectionString;

        public ClassRoomDbHelper(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString");
        }

        public async Task<ClassRoom> GetByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ClassRoom WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new ClassRoom
                            {
                                Id = (Guid)reader["Id"],
                                Name = reader["Name"].ToString(),
                                CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                                LastUpdatedDateTime = (DateTime)reader["LastUpdatedDateTime"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<ClassRoom>> GetAllAsync()
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ClassRoom", connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        classRooms.Add(new ClassRoom
                        {
                            Id = (Guid)reader["Id"],
                            Name = reader["Name"].ToString(),
                            CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                            LastUpdatedDateTime = (DateTime)reader["LastUpdatedDateTime"],
                            IsActive = (bool)reader["IsActive"]
                        });
                    }
                }
            }

            return classRooms;
        }

        public async Task<string> CreateAsync(ClassRoom classRoom)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO ClassRoom (Name, CreatedDateTime, LastUpdatedDateTime, IsActive) VALUES (@Name, @CreatedDateTime, @LastUpdatedDateTime, @IsActive)", connection))
                {
                    command.Parameters.AddWithValue("@Name", classRoom.Name);
                    command.Parameters.AddWithValue("@CreatedDateTime", classRoom.CreatedDateTime);
                    command.Parameters.AddWithValue("@LastUpdatedDateTime", classRoom.LastUpdatedDateTime);
                    command.Parameters.AddWithValue("@IsActive", classRoom.IsActive);

                    object result = await command.ExecuteScalarAsync();

                    if (result != null)
                    {
                        return "Success";
                    }
                }
            }

            return null;
        }

        public async Task<string> UpdateAsync(ClassRoom classRoom)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE ClassRoom SET Name = @Name, LastUpdatedDateTime = @LastUpdatedDateTime, IsActive = @IsActive WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", classRoom.Id);
                    command.Parameters.AddWithValue("@Name", classRoom.Name);
                    command.Parameters.AddWithValue("@LastUpdatedDateTime", classRoom.LastUpdatedDateTime);
                    command.Parameters.AddWithValue("@IsActive", classRoom.IsActive);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return "Success";
                    }
                }
            }

            return null;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM ClassRoom WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return "Deleted";
                    }
                }
            }

            return null;
        }
    }

}
