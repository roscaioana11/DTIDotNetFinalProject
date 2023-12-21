using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.DTOs.MarkDTOS;

public class MarkToGetFromTeacherDTO
{
    [Range(1, 10, ErrorMessage = "{0} can only be beteween {1} and {10}")]
    public int Value { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int StudentId { get; set; }
}