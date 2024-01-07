using Microsoft.Extensions.Configuration;
using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public class AllocatedClassRoomDbHelper : IAllocatedClassRoomDbHelper
    {
        private readonly string connectionString;

        public AllocatedClassRoomDbHelper(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString");
        }

        public async Task<string> CreateAsync(AllocatedClassRoom allocated)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO AllocatedClassRoom (TeacherId, ClassRoomId, CreatedDateTime) VALUES (@TeacherId, @ClassRoomId, @CreatedDateTime)", connection))
                {
                    command.Parameters.AddWithValue("@TeacherId", allocated.TeacherId);
                    command.Parameters.AddWithValue("@ClassRoomId", allocated.ClassRoomId);
                    command.Parameters.AddWithValue("@CreatedDateTime", allocated.CreatedDateTime);

                    object result = await command.ExecuteScalarAsync();

                    if (result != null)
                    {
                        return "Success";
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<ClassRoom>> GetByTeacherIdAsync(Guid id)
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                        SELECT c.*
                        FROM ClassRoom c
                        INNER JOIN (SELECT * FROM AllocatedClassRoom WHERE TeacherId = @Id) a ON c.Id = a.ClassRoomId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

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
            }

            return classRooms;
        }


        public async Task<IEnumerable<ClassRoom>> GetNotAllocatedClassRoomsByTeacherIdAsync(Guid id)
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                        SELECT c.*
                        FROM ClassRoom c
                        LEFT JOIN (SELECT * FROM AllocatedClassRoom WHERE TeacherId = @Id) a ON c.Id = a.ClassRoomId
                        WHERE a.Id IS NULL";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

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
            }

            return classRooms;
        }


        public async Task<string> DeleteByTeacherIdAndClassRoomIdAsync(Guid teacherId, Guid classRoomId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM AllocatedClassRoom WHERE TeacherId = @TeacherId AND ClassRoomId = @ClassRoomId", connection))
                {
                    command.Parameters.AddWithValue("@TeacherId", teacherId);
                    command.Parameters.AddWithValue("@ClassRoomId", classRoomId);

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

