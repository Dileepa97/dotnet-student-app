using System.ComponentModel.DataAnnotations;
using System;

namespace student_mgt_app.Models.DTO
{
    public class AllocatedClassRoomAddRequestDto
    {
        [Required]
        public Guid TeacherId { get; set; }

        [Required]
        public Guid ClassRoomId { get; set; }
    }
}
