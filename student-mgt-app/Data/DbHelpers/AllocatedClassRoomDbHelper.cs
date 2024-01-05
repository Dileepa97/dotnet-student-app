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

        public async Task<IEnumerable<AllocatedClassRoom>> GetByTeacherIdAsync(Guid id)
        {
            List<AllocatedClassRoom> classRooms = new List<AllocatedClassRoom>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AllocatedClassRoom WHERE TeacherId = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            classRooms.Add(new AllocatedClassRoom
                            {
                                Id = (Guid)reader["Id"],
                                TeacherId = (Guid)reader["TeacherId"],
                                ClassRoomId = (Guid)reader["ClassRoomId"],
                                CreatedDateTime = (DateTime)reader["CreatedDateTime"]
                            });
                        }
                    }
                }
            }

            return classRooms;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM AllocatedClassRoom WHERE Id = @Id", connection))
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

