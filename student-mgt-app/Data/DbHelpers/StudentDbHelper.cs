﻿using Microsoft.Extensions.Configuration;
using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public class StudentDbHelper : IStudentDbHelper
    {
        private readonly string connectionString;

        public StudentDbHelper(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString");
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = (Guid)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                ContactPerson = reader["ContactPerson"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                Email = reader["Email"].ToString(),
                                DOB = (DateTime)reader["DOB"],
                                ClassRoomId = (Guid)reader["ClassRoomId"],
                                CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                                LastUpdatedDateTime = (DateTime)reader["LastUpdatedDateTime"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }

                return null;
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Student", connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = (Guid)reader["Id"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            ContactPerson = reader["ContactPerson"].ToString(),
                            ContactNo = reader["ContactNo"].ToString(),
                            Email = reader["Email"].ToString(),
                            DOB = (DateTime)reader["DOB"],
                            ClassRoomId = (Guid)reader["ClassRoomId"],
                            CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                            LastUpdatedDateTime = (DateTime)reader["LastUpdatedDateTime"],
                            IsActive = (bool)reader["IsActive"]
                        });
                    }
                }
            }

            return students;
        }

        public async Task<string> CreateAsync(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO Student (FirstName, LastName, ContactPerson, ContactNo, Email, DOB, ClassRoomId, CreatedDateTime, LastUpdatedDateTime, IsActive) VALUES (@FirstName, @LastName, @ContactPerson, @ContactNo, @Email, @DOB, @ClassRoomId, @CreatedDateTime, @LastUpdatedDateTime, @IsActive)", connection))
                {
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@ContactPerson", student.ContactPerson);
                    command.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@DOB", student.DOB);
                    command.Parameters.AddWithValue("@ClassRoomId", student.ClassRoomId);
                    command.Parameters.AddWithValue("@CreatedDateTime", student.CreatedDateTime);
                    command.Parameters.AddWithValue("@LastUpdatedDateTime", student.LastUpdatedDateTime);
                    command.Parameters.AddWithValue("@IsActive", student.IsActive);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return "Success";
                    }
                }
            }

            return null;
        }

        public async Task<string> UpdateAsync(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE Student SET FirstName = @FirstName, LastName = @LastName, ContactPerson = @ContactPerson, ContactNo = @ContactNo, Email = @Email, DOB = @DOB, ClassRoomId = @ClassRoomId, LastUpdatedDateTime = @LastUpdatedDateTime, IsActive = @IsActive WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@ContactPerson", student.ContactPerson);
                    command.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@DOB", student.DOB);
                    command.Parameters.AddWithValue("@ClassRoomId", student.ClassRoomId);
                    command.Parameters.AddWithValue("@LastUpdatedDateTime", student.LastUpdatedDateTime);
                    command.Parameters.AddWithValue("@IsActive", student.IsActive);

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Student WHERE Id = @Id", connection))
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