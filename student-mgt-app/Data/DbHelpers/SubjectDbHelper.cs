using Microsoft.Extensions.Configuration;
using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public class SubjectDbHelper: ISubjectDbHelper
    {
        private readonly string connectionString;

        public SubjectDbHelper(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString");
        }

        public async Task<Subject> GetByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Subject WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Subject
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

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            List<Subject> subjects = new List<Subject>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Subject", connection))
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

            return subjects;
        }

        public async Task<string> CreateAsync(Subject subject)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO Subject (Name, CreatedDateTime, LastUpdatedDateTime, IsActive) VALUES (@Name, @CreatedDateTime, @LastUpdatedDateTime, @IsActive); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@Name", subject.Name);
                    command.Parameters.AddWithValue("@CreatedDateTime", subject.CreatedDateTime);
                    command.Parameters.AddWithValue("@LastUpdatedDateTime", subject.LastUpdatedDateTime);
                    command.Parameters.AddWithValue("@IsActive", subject.IsActive);

                    object result = await command.ExecuteScalarAsync();

                    if (result != null)
                    {
                        return "Success";
                    }
                }
            }

            return null;
        }

        public async Task<string> UpdateAsync(Subject subject)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE Subject SET Name = @Name, LastUpdatedDateTime = @LastUpdatedDateTime, IsActive = @IsActive WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", subject.Id);
                    command.Parameters.AddWithValue("@Name", subject.Name);
                    command.Parameters.AddWithValue("@LastUpdatedDateTime", subject.LastUpdatedDateTime);
                    command.Parameters.AddWithValue("@IsActive", subject.IsActive);

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Subject WHERE Id = @Id", connection))
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
