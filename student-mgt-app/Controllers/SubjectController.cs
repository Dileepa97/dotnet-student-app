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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectDbHelper subjectDbHelper;
        private readonly IMapper mapper;

        public SubjectController(ISubjectDbHelper subjectDbHelper, IMapper mapper)
        {
            this.subjectDbHelper = subjectDbHelper;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectAddRequestDto requestDto)
        {
            var subject = mapper.Map<Subject>(requestDto);

            subject.CreatedDateTime = DateTime.UtcNow;
            subject.LastUpdatedDateTime = DateTime.UtcNow;
            subject.IsActive = true;

            var result = await subjectDbHelper.CreateAsync(subject);

            if (result == null)
            {
                return NoContent();
            }

            return Created("", "Success");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await subjectDbHelper.GetAllAsync();

            return Ok(mapper.Map<List<SubjectDto>>(subjects));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var subject = await subjectDbHelper.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SubjectDto>(subject));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SubjectUpdateRequestDto requestDto)
        {
            var subjectExist = await subjectDbHelper.GetByIdAsync(id);

            if (subjectExist == null)
            {
                return NotFound();
            }

            var subject = mapper.Map<Subject>(requestDto);

            subject.Id = id;
            subject.CreatedDateTime = subjectExist.CreatedDateTime;
            subject.LastUpdatedDateTime = DateTime.UtcNow;
            subject.IsActive = subjectExist.IsActive;

            var result = await subjectDbHelper.UpdateAsync(subject);

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
            var deleted = await subjectDbHelper.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }

}
