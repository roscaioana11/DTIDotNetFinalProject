using OnlineClassbook.DTOs.CourseDTOS;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.Entities;

namespace OnlineClassbook.Utils;

public static class Utils
{
    public static StudentMarksAverageDTO ToAverageDto(this Student student, double average)
    {
        return new StudentMarksAverageDTO
        {
            Name = student.GetFullName(),
            Average = average,
        };
    }

    public static Address ToEntity(this AddressToUpdateDTO updateStudentAddress)
    {
        if (updateStudentAddress == null)
            return null;

        return new Address
        {
            Number = updateStudentAddress.Number,
            City = updateStudentAddress.City,
            Street = updateStudentAddress.Street
        };
    }

    public static CourseToGetDTO? ToDto(this Course course)
        => course is null
            ? null
            : new CourseToGetDTO
            {
                Id = course.Id,
                Name = course.Name
            };

    public static Course ToEntity(this CourseToCreateDTO course)
        => course is null ? null : new Course { Name = course.Name };


    public static MarkToGetDTO? ToDto(this Mark mark)
        => mark is null
            ? null
            : new MarkToGetDTO
            {
                Id = mark.Id,
                Value = mark.Value,
                CreatedAt = mark.CreatedAt,
                CourseId = mark.CourseId,
                CourseName = mark.Course.Name
            };

    public static Mark ToEntity(this MarkToCreateDTO mark)
        => mark is null
            ? null
            : new Mark
            {
                Value = mark.Value,
                CourseId = mark.CourseId,
                StudentId = mark.StudentId
            };
}