using System;
using System.ComponentModel.DataAnnotations;

namespace student_mgt_app.Models.DTO
{
    public class AllocatedSubjectAddRequestDto
    {
        [Required]
        public Guid TeacherId { get; set; }

        [Required]
        public Guid SubjectId { get; set; }
    }
}
