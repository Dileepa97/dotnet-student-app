using System.ComponentModel.DataAnnotations;
using System;

namespace student_mgt_app.Models.DTO
{
    public class StudentUpdateRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "FirstName has to be maximum of 50 chatacters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "LastName has to be maximum of 50 chatacters")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "ContactPerson has to be maximum of 50 chatacters")]
        public string ContactPerson { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "ContactNo has to be maximum of 20 chatacters")]
        public string ContactNo { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Email has to be maximum of 100 chatacters")]
        public string Email { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public Guid ClassRoomId { get; set; }
    }
}
