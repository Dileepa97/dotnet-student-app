using System;
using System.Runtime.Serialization;

namespace student_mgt_app.Models.Domain
{

    public class Teacher
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
