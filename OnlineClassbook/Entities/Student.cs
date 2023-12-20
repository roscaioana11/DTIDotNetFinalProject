using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.Entities;

public class Student
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();
    public List<Mark> Marks { get; set; } = new List<Mark>();

    public string GetFullName()
    {
        return $"{LastName} {FirstName}";
    }
}
