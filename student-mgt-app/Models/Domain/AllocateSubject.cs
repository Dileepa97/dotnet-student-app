using System;
using System.Runtime.Serialization;

namespace student_mgt_app.Models.Domain
{
    [DataContract]
    public class AllocateSubject
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "TeacherId")]
        public Guid TeacherId { get; set; }

        [DataMember(Name = "SubjectId")]
        public Guid SubjectId { get; set; }

        [DataMember(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
