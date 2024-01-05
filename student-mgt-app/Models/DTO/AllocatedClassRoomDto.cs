using System;

namespace student_mgt_app.Models.DTO
{
    public class AllocatedClassRoomDto
    {
        public Guid Id { get; set; }

        public Guid ClassRoomId { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
