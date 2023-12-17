using OnlineClassbook.DTOs;
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
}