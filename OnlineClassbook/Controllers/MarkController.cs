using Microsoft.AspNetCore.Mvc;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.Entities;
using OnlineClassbook.Services;
using OnlineClassbook.Utils;

namespace OnlineClassbook.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MarkController:ControllerBase
{
    private readonly IStudentService _studentService;

    public MarkController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    /// <summary>
    /// Creates a mark
    /// </summary>
    /// <param name="markCreate">Student to create data</param>
    /// <returns>The created mark's data</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
    public async Task<ActionResult<MarkToGetDTO>> AddMarks([FromBody] MarkToCreateDTO markCreate) => 
        Ok(await _studentService.AddMarks(markCreate));
    
    /// <summary>
    /// Get marks for a student.
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>A list of marks</returns>
    [HttpGet("{studentId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<MarkToGetDTO>>> GetMarksByStudentId(int studentId) => 
        Ok(await _studentService.GetMarksByStudentId(studentId));
    
    /// <summary>
    /// Get marks for a student for the specified course.
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="courseId"></param>
    /// <returns>A list of marks</returns>
    [HttpGet("{studentId}/{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<MarkToGetDTO>>> GetMarksByCourseIdAndStudentId(int studentId, int courseId) =>
        Ok(await _studentService.GetMarksByCourseIdAndStudentId(studentId, courseId));
    
    /// <summary>
    /// Get a student's average marks for the specified course.
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="courseId"></param>
    /// <returns>A double, average result</returns>
    [HttpGet("{studentId}/{courseId}/average")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetAverageStudentCourseMarks(int studentId, int courseId) => 
        Ok(await _studentService.GetAverageStudentCourseMarks(studentId, courseId));

    /// <summary>
    /// Gets a list of students ordered by marks average.
    /// </summary>
    /// <param name="sort"></param>
    /// <returns>List of students with marks average</returns>
    [HttpGet("average")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    public async Task<ActionResult<List<StudentMarksAverageDTO>>> GetStudentOrderByMarksAverage([FromQuery] Sorting sort) => 
        Ok(await _studentService.GetStudentOrderByMarksAverage((student, average) => student.ToAverageDto(average), sort));
}