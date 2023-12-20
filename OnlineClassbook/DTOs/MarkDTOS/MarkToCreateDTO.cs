using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.DTOs.MarkDTOS;

public class MarkToCreateDTO
{
    [Range(1, 10, ErrorMessage = "{0} can only be beteween {1} and {10}")]
    public int Value { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}