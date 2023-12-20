using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.DTOs.StudentDTOS;

public class StudentToCreateDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name cannot be empty!")]
    public string LastName { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name cannot be empty!")]
    public string FirstName { get; set; }

    [Range(1,100)]
    public int Age { get; set; }
}