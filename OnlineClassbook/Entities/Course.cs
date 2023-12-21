namespace OnlineClassbook.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
    public List<Mark> Marks { get; set; } = new List<Mark>();
    public Teacher? Teacher { get; set; }
    public int? TeacherId { get; set; }
}