using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.TeacherDTOS;
using OnlineClassbook.Entities;

namespace OnlineClassbook.Services;

public interface ITeacherService
{
    Task<TeacherToGetDTO> AddTeacher(TeacherToCreateDTO newTeacher);
    Task<List<TeacherToGetDTO>> DeleteTeacherById(int id);
    Task<bool> UpdateOrCreateTeacherAddress(int id, Address newAddress);
    Task<string> AssignTeacherToCourse(int id, int courseId);
    Task<TeacherToGetDTO> PromoteTeacher(int id, string teacherNewRank);
    Task<List<MarkToGetFromTeacherDTO>> GetTeacherGivenMarks(int teacherId);
}