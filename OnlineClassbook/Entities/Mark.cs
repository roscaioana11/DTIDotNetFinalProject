using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.Entities;

public class Mark
{
    public int Id { get; set; }
    [Range(1,10, ErrorMessage = "{0} can only be beteween {1} and {10}")]
    public int Value { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
        
    public int? CourseId { get; set; }
    public Course Course { get; set; }
        
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
}