using AutoMapper;
using student_mgt_app.Models.Domain;
using student_mgt_app.Models.DTO;

namespace student_mgt_app.Utility
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<TeacherAddRequestDto, Teacher>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();

            CreateMap<TeacherUpdateRequestDto, Teacher>().ReverseMap();

            CreateMap<ClassRoomAddRequestDto, ClassRoom>().ReverseMap();

            CreateMap<ClassRoom, ClassRoomDto>().ReverseMap();

            CreateMap<ClassRoomUpdateRequestDto, ClassRoom>().ReverseMap();
        }
    }
}
