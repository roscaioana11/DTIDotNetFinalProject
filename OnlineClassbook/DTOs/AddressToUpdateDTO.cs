using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.DTOs;

/// <summary>
/// Addres that will be used for update
/// </summary>
public class AddressToUpdateDTO
{
    /// <summary>
    /// City name
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "City cannot be empty!")]
    public string City { get; set; }
    
    /// <summary>
    /// Street name
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Street cannot be empty!")]
    public string Street { get; set; }
    
    /// <summary>
    /// Street number
    /// </summary>
    [Range(1,int.MaxValue)]
    public int Number { get; set; }
}