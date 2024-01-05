using System;

namespace student_mgt_app.Models.DTO
{
    public class AllocatedSubjectDto
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
