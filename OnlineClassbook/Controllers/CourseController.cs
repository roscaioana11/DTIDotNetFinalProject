using Microsoft.AspNetCore.Mvc;
using OnlineClassbook.DTOs.CourseDTOS;
using OnlineClassbook.Services;

namespace OnlineClassbook.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController:ControllerBase
{
    private readonly IStudentService _studentService;

    public CourseController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Gets a list with all courses.
    /// </summary>
    /// <returns>List with course data</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    public async Task<ActionResult<List<CourseToGetDTO>>> GetAllCourses() =>
        Ok(await _studentService.GetAllCourses());
    
    /// <summary>
    /// Creates a course.
    /// </summary>
    /// <param name="courseToCreate"></param>
    /// <returns>The created course data</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
    public async Task<ActionResult<CourseToGetDTO>> AddCourse([FromBody] CourseToCreateDTO courseToCreate) => 
        Ok(await _studentService.AddCourse(courseToCreate));
    
    /// <summary>
    /// Deletes a course by id
    /// </summary>
    /// <param name="courseId">Teacher id</param>
    /// <returns>List with courses data</returns>
    [HttpDelete("courseId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<List<CourseToGetDTO>>> DeleteCourseById(int courseId) =>
        Ok(await _studentService.DeleteCourseById(courseId));
    
}