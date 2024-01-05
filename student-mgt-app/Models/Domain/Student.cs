using System;
using System.Runtime.Serialization;

namespace student_mgt_app.Models.Domain
{
    [DataContract]
    public class Student
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        [DataMember(Name = "ContactPerson")]
        public string ContactPerson { get; set; }

        [DataMember(Name = "ContactNo")]
        public string ContactNo { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "DOB")]
        public DateTime DOB { get; set; }

        [DataMember(Name = "ClassRoomId")]
        public Guid ClassRoomId { get; set; }

        [DataMember(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [DataMember(Name = "LastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }

        [DataMember(Name = "IsActive")]
        public bool IsActive { get; set; }


        // Navigation properties
        public ClassRoom ClassRoom { get; set; }

    }
}
