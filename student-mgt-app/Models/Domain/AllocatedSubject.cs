using System;

namespace student_mgt_app.Models.Domain
{
    public class AllocatedSubject
    {
        public Guid Id { get; set; }

        public Guid TeacherId { get; set; }

        public Guid SubjectId { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
