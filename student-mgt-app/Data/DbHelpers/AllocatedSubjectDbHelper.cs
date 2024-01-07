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

        public async Task<IEnumerable<Subject>> GetByTeacherIdAsync(Guid id)
        {
            List<Subject> subjects = new List<Subject>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                        SELECT s.*
                        FROM Subject s
                        INNER JOIN (SELECT * FROM AllocatedSubject WHERE TeacherId = @Id) a ON s.Id = a.SubjectId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
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

            return subjects;
        }

        public async Task<IEnumerable<Subject>> GetNotAllocatedSubjectsByTeacherIdAsync(Guid id)
        {
            List<Subject> subjects = new List<Subject>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                        SELECT s.*
                        FROM Subject s
                        LEFT JOIN (SELECT * FROM AllocatedSubject WHERE TeacherId = @Id) a ON s.Id = a.SubjectId
                        WHERE a.Id IS NULL";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
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

            return subjects;
        }

        public async Task<string> DeleteByTeacherIdAndSubjectIdAsync(Guid teacherId, Guid subjectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM AllocatedSubject WHERE TeacherId = @TeacherId AND SubjectId = @SubjectId", connection))
                {
                    command.Parameters.AddWithValue("@TeacherId", teacherId);
                    command.Parameters.AddWithValue("@SubjectId", subjectId);

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
