namespace OnlineClassbook.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public TeacherRank Rank { get; set; }
    public Course? Course { get; set; }
}