using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.DTOs.TeacherDTOS;
using OnlineClassbook.Services;
using OnlineClassbook.Utils;

namespace OnlineClassbook.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    /// <summary>
    /// Creates a teacher
    /// </summary>
    /// <param name="newTeacher">Teacher to create data</param>
    /// <returns>List with teachers data</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherToGetDTO))]
    public async Task<ActionResult<TeacherToGetDTO>> Add([FromBody] TeacherToCreateDTO newTeacher) =>
        Ok(await _teacherService.AddTeacher(newTeacher));
    
    /// <summary>
    /// Deletes a teacher by id
    /// </summary>
    /// <param name="id">Teacher id</param>
    /// <returns>List with students data</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeacherToGetDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<TeacherToGetDTO>>> DeleteTeacherById([FromRoute] int id) =>
        Ok(await _teacherService.DeleteTeacherById(id));

    /// <summary>
    /// Update or create a student's address
    /// </summary>
    /// <param name="id">Teacher id</param>
    /// <param name="updateTeacherAddress">Teacher address to be updated</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateTeacherAddress([FromRoute] int id, AddressToUpdateDTO updateTeacherAddress)
    {
        bool updateOrCreateTeacherAddress =
            await _teacherService.UpdateOrCreateTeacherAddress(id, updateTeacherAddress.ToEntity());
        if (updateOrCreateTeacherAddress)
        {
            return Created($"Address for teacher with ID {id} was created!", null);
        }

        return Ok();
    }

    /// <summary>
    /// Assigns a teacher to a course
    /// </summary>
    /// <param name="id">Teacher id</param>
    /// <param name="courseId">Course id</param>
    [HttpGet("{id}/{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> AssignTeacherToCourse([FromRoute] [Required] int id,
        [FromRoute] [Required] int courseId) =>
        Ok(await _teacherService.AssignTeacherToCourse(id, courseId));

    /// <summary>
    /// Promote a teacher
    /// </summary>
    /// <param name="id">Teacher id</param>
    /// <param name="teacherNewRank">Teacher new rank</param>
    /// <returns>Promoted teacher</returns>
    [HttpPatch("{id}/promote")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<TeacherToGetDTO>> PromoteTeacher([FromRoute] int id,
        [FromBody] string teacherNewRank) =>
        Ok(await _teacherService.PromoteTeacher(id, teacherNewRank));

    /// <summary>
    /// Promote a teacher
    /// </summary>
    /// <param name="teacherId">Teacher id</param>
    /// <returns>List of student marks given by the teacher</returns>
    [HttpGet("{teacherId}/marks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<MarkToGetFromTeacherDTO>>> GetTeacherGivenMarks([FromRoute] int teacherId) => 
        Ok(await _teacherService.GetTeacherGivenMarks(teacherId));
}