using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.Services;
using OnlineClassbook.Utils;

namespace OnlineClassbook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController:ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    /// <summary>
    /// Returns all students from the db
    /// </summary>
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGetDTO>))]
    public async Task<ActionResult<List<StudentToGetDTO>>> GetAll() =>
        Ok(await _studentService.GetAllStudents());  
    

    /// <summary>
    /// Gets a student by id
    /// </summary>
    /// <param name="id">Id of the student</param>
    /// <returns>student data</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<StudentToGetDTO>> GetById(int id) =>
        Ok(await _studentService.GetStudentById(id));
    

    /// <summary>
    /// Creates a student
    /// </summary>
    /// <param name="newStudent">Student to create data</param>
    /// <returns>List with students data</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGetDTO>))]
    public async Task<ActionResult<List<StudentToGetDTO>>> Add([FromBody] StudentToCreateDTO newStudent) =>
        Ok(await _studentService.AddStudent(newStudent));
    
    /// <summary>
    /// Deletes a student by id
    /// </summary>
    /// <param name="id">Id of the student</param>
    /// <returns>List with students data</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGetDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<StudentToGetDTO>>> DeleteById(int id) =>
        Ok(await _studentService.DeleteStudentById(id));
    
    /// <summary>
    /// Updates a student
    /// </summary>
    /// <param name="updateStudent">Student to be updated</param>
    /// <returns>List with students data</returns>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGetDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<StudentToGetDTO>>> UpdateStudent([FromBody] StudentToUpdateDTO updateStudent) =>
        Ok(await _studentService.UpdateStudent(updateStudent));
    
    /// <summary>
    /// Update or create a student's address
    /// </summary>
    /// <param name="id">Student id</param>
    /// <param name="updateStudentAddress">Student address to be updated</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateStudentAddress([FromRoute] int id, [FromBody] AddressToUpdateDTO updateStudentAddress)
    {
        bool updateOrCreateStudentAddress = await _studentService.UpdateOrCreateStudentAddress(id, updateStudentAddress.ToEntity());
        if (updateOrCreateStudentAddress)
        {
            return Created($"Address for student with ID {id} was created!", null);
        }
        return Ok();
    }

    /// <summary>
    /// Assigns a student to a course
    /// </summary>
    /// <param name="studentId">Student id</param>
    /// <param name="courseId">Course id</param>
    [HttpGet("assign")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> AssignStudentToCourse([Required] int studentId,[Required] int courseId) => 
        Ok(await _studentService.AssignStudentToCourse(studentId, courseId));

}