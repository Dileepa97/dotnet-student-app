using System;

namespace student_mgt_app.Models.DTO
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }

        public int Age { get; set; }

        public Guid ClassRoomId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastUpdatedDateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
