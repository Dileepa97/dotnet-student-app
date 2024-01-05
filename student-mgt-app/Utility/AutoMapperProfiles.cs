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

            CreateMap<SubjectAddRequestDto, Subject>().ReverseMap();

            CreateMap<Subject, SubjectDto>().ReverseMap();

            CreateMap<SubjectUpdateRequestDto, Subject>().ReverseMap();

            CreateMap<StudentAddRequestDto, Student>().ReverseMap();

            CreateMap<Student, StudentDto>().ReverseMap();

            CreateMap<StudentUpdateRequestDto, Student>().ReverseMap();

            CreateMap<AllocatedSubjectAddRequestDto, AllocatedSubject>().ReverseMap();

            CreateMap<AllocatedSubject, AllocatedSubjectDto>().ReverseMap();

            CreateMap<AllocatedClassRoomAddRequestDto, AllocatedClassRoom>().ReverseMap();

            CreateMap<AllocatedClassRoom, AllocatedClassRoomDto>().ReverseMap();
        }
    }
}
