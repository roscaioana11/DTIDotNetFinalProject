using OnlineClassbook.DTOs.CourseDTOS;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.Entities;

namespace OnlineClassbook;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Student, StudentToGetDTO>();
        CreateMap<StudentToCreateDTO, Student>();
        //
        // CreateMap<CourseToCreateDTO, Course>();
        // CreateMap<Course, CourseToCreateDTO>();
        //
        // CreateMap<Mark, MarkToGetDTO>();
        // CreateMap<MarkToCreateDTO, Mark>();
    }
    
}