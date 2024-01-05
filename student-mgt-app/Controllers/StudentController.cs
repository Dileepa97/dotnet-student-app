using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_mgt_app.Data.DbHelpers;
using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using student_mgt_app.Models.DTO;

namespace student_mgt_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentDbHelper studentDbHelper;
        private readonly IMapper mapper;

        public StudentController(IStudentDbHelper studentDbHelper, IMapper mapper)
        {
            this.studentDbHelper = studentDbHelper;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentAddRequestDto requestDto)
        {
            var student = mapper.Map<Student>(requestDto);

            student.CreatedDateTime = DateTime.UtcNow;
            student.LastUpdatedDateTime = DateTime.UtcNow;
            student.IsActive = true;

            var result = await studentDbHelper.CreateAsync(student);

            if (result == null)
            {
                return NoContent();
            }

            return Created("", "Success");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await studentDbHelper.GetAllAsync();

            return Ok(mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var student = await studentDbHelper.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StudentDto>(student));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] StudentUpdateRequestDto requestDto)
        {
            var studentExist = await studentDbHelper.GetByIdAsync(id);

            if (studentExist == null)
            {
                return NotFound();
            }

            var student = mapper.Map<Student>(requestDto);
            student.Id = id;
            student.CreatedDateTime = studentExist.CreatedDateTime;
            student.LastUpdatedDateTime = DateTime.UtcNow;
            student.IsActive = studentExist.IsActive;

            var result = await studentDbHelper.UpdateAsync(student);

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
            var deleted = await studentDbHelper.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }

}
