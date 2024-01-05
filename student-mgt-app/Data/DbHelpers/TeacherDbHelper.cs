using Microsoft.Extensions.Configuration;
using student_mgt_app.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace student_mgt_app.Data.DbHelpers
{
    public class TeacherDbHelper : ITeacherDbHelper
    {
        private readonly string connectionString;

        public TeacherDbHelper(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString"); ;
        }

        public async Task<Teacher> GetByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Teacher WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Teacher
                            {
                                Id = (Guid)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                Email = reader["Email"].ToString(),
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                LastUpdatedDate = (DateTime)reader["LastUpdatedDate"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Teacher", connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        teachers.Add(new Teacher
                        {
                            Id = (Guid)reader["Id"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            ContactNo = reader["ContactNo"].ToString(),
                            Email = reader["Email"].ToString(),
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            LastUpdatedDate = (DateTime)reader["LastUpdatedDate"],
                            IsActive = (bool)reader["IsActive"]
                        });
                    }
                }
            }

            return teachers;
        }

        public async Task<string> CreateAsync(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO Teacher (FirstName, LastName, ContactNo, Email, CreatedDate, LastUpdatedDate, IsActive) VALUES (@FirstName, @LastName, @ContactNo, @Email, @CreatedDate, @LastUpdatedDate, @IsActive)", connection))
                {
                    command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    command.Parameters.AddWithValue("@LastName", teacher.LastName);
                    command.Parameters.AddWithValue("@ContactNo", teacher.ContactNo);
                    command.Parameters.AddWithValue("@Email", teacher.Email);
                    command.Parameters.AddWithValue("@CreatedDate", teacher.CreatedDate);
                    command.Parameters.AddWithValue("@LastUpdatedDate", teacher.LastUpdatedDate);
                    command.Parameters.AddWithValue("@IsActive", teacher.IsActive);

                    object result = await command.ExecuteScalarAsync();

                    if (result != null)
                    {
                        return "Success";
                    }
                }
            }

            return null;
        }

        public async Task<string> UpdateAsync(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE Teacher SET FirstName = @FirstName, LastName = @LastName, ContactNo = @ContactNo, Email = @Email, LastUpdatedDate = @LastUpdatedDate, IsActive = @IsActive WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", teacher.Id);
                    command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    command.Parameters.AddWithValue("@LastName", teacher.LastName);
                    command.Parameters.AddWithValue("@ContactNo", teacher.ContactNo);
                    command.Parameters.AddWithValue("@Email", teacher.Email);
                    command.Parameters.AddWithValue("@LastUpdatedDate", teacher.LastUpdatedDate);
                    command.Parameters.AddWithValue("@IsActive", teacher.IsActive);

                    object result = await command.ExecuteScalarAsync();

                    if (result != null)
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

                using (SqlCommand command = new SqlCommand("DELETE FROM Teacher WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    object result = await command.ExecuteScalarAsync();

                    if (result != null)
                    {
                        return "Deleted";
                    }
                }
            }

            return null;
        }
    }
}
