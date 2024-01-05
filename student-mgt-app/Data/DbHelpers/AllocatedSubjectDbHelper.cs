using Microsoft.Extensions.Configuration;
using student_mgt_app.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace student_mgt_app.Data.DbHelpers
{
    public class AllocatedSubjectDbHelper : IAllocatedSubjectDbHelper
    {
        private readonly string connectionString;

        public AllocatedSubjectDbHelper(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString");
        }

        public async Task<string> CreateAsync(AllocatedSubject allocated)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO AllocatedSubject (TeacherId, SubjectId, CreatedDateTime) VALUES (@TeacherId, @SubjectId, @CreatedDateTime)", connection))
                {
                    command.Parameters.AddWithValue("@TeacherId", allocated.TeacherId);
                    command.Parameters.AddWithValue("@SubjectId", allocated.SubjectId);
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

        public async Task<IEnumerable<AllocatedSubject>> GetByTeacherIdAsync(Guid id)
        {
            List<AllocatedSubject> subjects = new List<AllocatedSubject>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AllocatedSubject WHERE TeacherId = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new AllocatedSubject
                            {
                                Id = (Guid)reader["Id"],
                                TeacherId = (Guid)reader["TeacherId"],
                                SubjectId = (Guid)reader["SubjectId"],
                                CreatedDateTime = (DateTime)reader["CreatedDateTime"]
                            });
                        }
                    }
                }
            }

            return subjects;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM AllocatedSubject WHERE Id = @Id", connection))
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
