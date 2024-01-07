using Microsoft.Extensions.Configuration;
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

                string query = @"
                        SELECT s.*, c.*
                        FROM Student s
                        INNER JOIN ClassRoom c ON s.ClassRoomId = c.Id
                        WHERE s.Id = @StudentId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            Student student = new Student
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

                            ClassRoom classRoom = new ClassRoom
                            {
                                Id = (Guid)reader["Id"],
                                Name = reader["Name"].ToString(),
                                CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                                LastUpdatedDateTime = (DateTime)reader["LastUpdatedDateTime"],
                                IsActive = (bool)reader["IsActive"]
                            };

                            student.ClassRoom = classRoom;

                            // Calculate age
                            DateTime currentDate = DateTime.UtcNow;
                            int age = currentDate.Year - student.DOB.Year;

                            if (currentDate < student.DOB.AddYears(age))
                            {
                                age--;
                            }

                            student.Age = age;

                            return student;
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

                string query = @"
                        SELECT s.*, c.*
                        FROM Student s
                        LEFT JOIN ClassRoom c ON s.ClassRoomId = c.Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
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

                        ClassRoom classRoom = new ClassRoom
                        {
                            Id = (Guid)reader["Id"],
                            Name = reader["Name"].ToString(),
                            CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                            LastUpdatedDateTime = (DateTime)reader["LastUpdatedDateTime"],
                            IsActive = (bool)reader["IsActive"]
                        };

                        student.ClassRoom = classRoom;

                        // Calculate age
                        DateTime currentDate = DateTime.UtcNow;
                        int age = currentDate.Year - student.DOB.Year;

                        if (currentDate < student.DOB.AddYears(age))
                        {
                            age--;
                        }

                        student.Age = age;

                        students.Add(student);
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


        //Get student report data
        public async Task<IEnumerable<Object>> GetStudentReportDataAsync(Guid id)
        {
            List<Object> data = new List<Object>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT TeacherId, FirstName, LastName, SubjectId, s.Name
                                FROM Subject s JOIN
                                (
	                                SELECT TeacherId, FirstName, LastName, SubjectId
	                                FROM Teacher t JOIN
	                                (
		                                SELECT TeacherId, SubjectId
		                                FROM AllocatedSubject
		                                WHERE TeacherId IN
		                                (
			                                SELECT TeacherId
			                                FROM AllocatedClassRoom
			                                WHERE ClassRoomId = @Id
		                                )
	                                ) AsAc ON t.Id = AsAc.TeacherId
                                ) AsAcT ON s.Id = AsAcT.SubjectId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var reportData = new 
                            {
                                TeacherId = (Guid)reader["TeacherId"],
                                TeacherFirstName = reader["FirstName"].ToString(),
                                TeacherLastName = reader["LastName"].ToString(),
                                SubjectId = (Guid)reader["SubjectId"],
                                SubjectName = reader["Name"].ToString()
                            };


                            data.Add(reportData);
                        }
                    }
                }

                return data;
            }
        }
    }

}
