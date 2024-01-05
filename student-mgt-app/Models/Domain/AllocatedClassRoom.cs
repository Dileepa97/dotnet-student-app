using System;

namespace student_mgt_app.Models.Domain
{
    public class AllocatedClassRoom
    {
        public Guid Id { get; set; }

        public Guid TeacherId { get; set; }

        public Guid ClassRoomId { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
