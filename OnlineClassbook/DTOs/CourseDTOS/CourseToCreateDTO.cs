using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.DTOs.CourseDTOS;

public class CourseToCreateDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Course name cannot be empty")]
    public string Name { get; set; }
}