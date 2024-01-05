using System.ComponentModel.DataAnnotations;

namespace student_mgt_app.Models.DTO
{
    public class SubjectUpdateRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name has to be maximum of 50 chatacters")]
        public string Name { get; set; }
    }
}
