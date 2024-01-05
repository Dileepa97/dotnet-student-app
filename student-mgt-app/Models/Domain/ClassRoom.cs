using System.Runtime.Serialization;
using System;

namespace student_mgt_app.Models.Domain
{
    [DataContract]
    public class ClassRoom
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [DataMember(Name = "LastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }

        [DataMember(Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}
