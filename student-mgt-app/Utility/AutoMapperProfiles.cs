using AutoMapper;
using student_mgt_app.Models.Domain;
using student_mgt_app.Models.DTO;

namespace student_mgt_app.Utility
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AddTeacherRequestDto, Teacher>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();

            CreateMap<UpdateTeacherRequestDto, Teacher>().ReverseMap();
        }
    }
}
