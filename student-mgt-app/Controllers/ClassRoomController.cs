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
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomDbHelper classRoomDbHelper;
        private readonly IMapper mapper;

        public ClassRoomController(IClassRoomDbHelper classRoomDbHelper, IMapper mapper)
        {
            this.classRoomDbHelper = classRoomDbHelper;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassRoomAddRequestDto requestDto)
        {
            var classRoom = mapper.Map<ClassRoom>(requestDto);

            classRoom.CreatedDateTime = DateTime.UtcNow;
            classRoom.LastUpdatedDateTime = DateTime.UtcNow;
            classRoom.IsActive = true;

            var result = await classRoomDbHelper.CreateAsync(classRoom);

            if (result == null)
            {
                return NoContent();
            }

            return Created("", "Success");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classRooms = await classRoomDbHelper.GetAllAsync();

            return Ok(mapper.Map<List<ClassRoomDto>>(classRooms));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var classRoom = await classRoomDbHelper.GetByIdAsync(id);

            if (classRoom == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ClassRoomDto>(classRoom));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ClassRoomUpdateRequestDto requestDto)
        {
            var classRoomExist = await classRoomDbHelper.GetByIdAsync(id);

            if (classRoomExist == null)
            {
                return NotFound();
            }

            var classRoom = mapper.Map<ClassRoom>(requestDto);
            classRoom.Id = id;
            classRoom.CreatedDateTime = classRoomExist.CreatedDateTime;
            classRoom.LastUpdatedDateTime = DateTime.UtcNow;
            classRoom.IsActive = classRoomExist.IsActive;

            var result = await classRoomDbHelper.UpdateAsync(classRoom);

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
            var deleted = await classRoomDbHelper.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }

}
