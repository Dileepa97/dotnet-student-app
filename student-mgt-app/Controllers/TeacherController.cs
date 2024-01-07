using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using student_mgt_app.Data.DbHelpers;
using student_mgt_app.Models.Domain;
using student_mgt_app.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace student_mgt_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherDbHelper teacherDbHelper;
        private readonly IAllocatedSubjectDbHelper allocatedSubjectDbHelper;
        private readonly IAllocatedClassRoomDbHelper allocatedClassRoomDbHelper;
        private readonly IMapper mapper;

        public TeacherController(ITeacherDbHelper teacherDbHelper, IAllocatedSubjectDbHelper allocatedSubjectDbHelper, IAllocatedClassRoomDbHelper allocatedClassRoomDbHelper, IMapper mapper)
        {
            this.teacherDbHelper = teacherDbHelper;
            this.mapper = mapper;
            this.allocatedSubjectDbHelper = allocatedSubjectDbHelper;
            this.allocatedClassRoomDbHelper = allocatedClassRoomDbHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherAddRequestDto requestDto)
        {
            var teacher = mapper.Map<Teacher>(requestDto);

            teacher.CreatedDate = DateTime.UtcNow;
            teacher.LastUpdatedDate = DateTime.UtcNow;
            teacher.IsActive = true;

            var result = await teacherDbHelper.CreateAsync(teacher);

            if (result == null)
            {
                return NoContent();
            }

            return Created("", "Success");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await teacherDbHelper.GetAllAsync();

            return Ok(mapper.Map<List<TeacherDto>>(teachers));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var teacher = await teacherDbHelper.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TeacherDto>(teacher));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TeacherUpdateRequestDto requestDto)
        {
            var teacherExist = await teacherDbHelper.GetByIdAsync(id);

            if (teacherExist == null)
            {
                return NotFound();
            }

            var teacher = mapper.Map<Teacher>(requestDto);
            teacher.Id = id;
            teacher.CreatedDate = teacherExist.CreatedDate;
            teacher.LastUpdatedDate = DateTime.UtcNow;
            teacher.IsActive = teacherExist.IsActive;

            var result = await teacherDbHelper.UpdateAsync(teacher);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delted = await teacherDbHelper.DeleteAsync(id);

            if (delted == null)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpGet]
        [Route("allocated-subjects/{id:Guid}")]
        public async Task<IActionResult> GetAllocatedSubjectsByTeacherId([FromRoute] Guid id)
        {
            var subjects = await allocatedSubjectDbHelper.GetByTeacherIdAsync(id);

            return Ok(mapper.Map<List<SubjectDto>>(subjects));
        }

        [HttpGet]
        [Route("not-allocated-subjects/{id:Guid}")]
        public async Task<IActionResult> GetNotAllocatedSubjectsByTeacherId([FromRoute] Guid id)
        {
            var subjects = await allocatedSubjectDbHelper.GetNotAllocatedSubjectsByTeacherIdAsync(id);

            return Ok(mapper.Map<List<SubjectDto>>(subjects));
        }

        [HttpPost]
        [Route("allocated-subjects")]
        public async Task<IActionResult> AllocateSubject([FromBody] AllocatedSubjectAddRequestDto requestDto)
        {
            var subject = mapper.Map<AllocatedSubject>(requestDto);

            subject.CreatedDateTime = DateTime.UtcNow;

            var result = await allocatedSubjectDbHelper.CreateAsync(subject);

            if (result == null)
            {
                return NoContent();
            }

            return Created("", "Success");
        }

        [HttpDelete]
        [Route("{teacherId:Guid}/deallocate-subject/{subjectId:Guid}")]
        public async Task<IActionResult> DeallocateSubject([FromRoute] Guid teacherId, [FromRoute] Guid subjectId)
        {
            var deleted = await allocatedSubjectDbHelper.DeleteByTeacherIdAndSubjectIdAsync(teacherId, subjectId);

            if (deleted == null)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpGet]
        [Route("allocated-classrooms/{id:Guid}")]
        public async Task<IActionResult> GetAllocatedClassRoomsByTeacherId([FromRoute] Guid id)
        {
            var classes = await allocatedClassRoomDbHelper.GetByTeacherIdAsync(id);

            return Ok(mapper.Map<List<AllocatedClassRoomDto>>(classes));
        }

        [HttpPost]
        [Route("allocated-classrooms")]
        public async Task<IActionResult> AllocateClassRoom([FromBody] AllocatedClassRoomAddRequestDto requestDto)
        {
            var classRoom = mapper.Map<AllocatedClassRoom>(requestDto);

            classRoom.CreatedDateTime = DateTime.UtcNow;

            var result = await allocatedClassRoomDbHelper.CreateAsync(classRoom);

            if (result == null)
            {
                return NoContent();
            }

            return Created("", "Success");
        }

        [HttpDelete]
        [Route("allocated-classrooms/{id:Guid}")]
        public async Task<IActionResult> DeallocateClassRoom([FromRoute] Guid id)
        {
            var deleted = await allocatedClassRoomDbHelper.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
