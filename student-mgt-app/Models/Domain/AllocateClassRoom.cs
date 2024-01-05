using System;
using System.Runtime.Serialization;

namespace student_mgt_app.Models.Domain
{
    [DataContract]
    public class AllocateClassRoom
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "TeacherId")]
        public Guid TeacherId { get; set; }

        [DataMember(Name = "ClassRoomId")]
        public Guid ClassRoomId { get; set; }

        [DataMember(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
