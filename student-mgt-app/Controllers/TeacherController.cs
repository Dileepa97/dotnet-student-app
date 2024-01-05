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
        private readonly IMapper mapper;

        public TeacherController(ITeacherDbHelper teacherDbHelper, IMapper mapper)
        {
            this.teacherDbHelper = teacherDbHelper;
            this.mapper = mapper;
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
    }
}
