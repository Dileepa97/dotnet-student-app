using System;

namespace student_mgt_app.Models.Domain
{
    public class ClassRoom
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastUpdatedDateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
