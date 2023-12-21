using OnlineClassbook.DTOs.CourseDTOS;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.Entities;

namespace OnlineClassbook.Services;

public interface IStudentService
{
    Task<List<StudentToGetDTO>> GetAllStudents();
    Task<StudentToGetDTO> GetStudentById(int id);
    Task<List<StudentToGetDTO>> AddStudent(StudentToCreateDTO newStudent);
    Task<List<StudentToGetDTO>> DeleteStudentById(int id);
    Task<List<StudentToGetDTO>> UpdateStudent(StudentToUpdateDTO updateStudent);
    Task<bool>UpdateOrCreateStudentAddress(int studentId, Address newAddress);
    
    
    Task<CourseToGetDTO> AddCourse(CourseToCreateDTO courseToCreate);
    Task<List<CourseToGetDTO>> GetAllCourses();
    Task<string> AssignStudentToCourse(int studentId, int courseId);
    
    
    Task<MarkToGetDTO> AddMarks(MarkToCreateDTO markCreate);
    Task<List<MarkToGetDTO>> GetMarksByStudentId(int studentId);
    Task<List<MarkToGetDTO>> GetMarksByCourseIdAndStudentId(int studentId, int courseId);
    Task<double> GetAverageStudentCourseMarks(int studentId, int courseId);
    Task<List<T>> GetStudentOrderByMarksAverage<T>(Func<Student,double,T> select, Sorting sort);
    Task<List<CourseToGetDTO>> DeleteCourseById(int courseId);
}