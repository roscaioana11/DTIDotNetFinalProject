using OnlineClassbook.Data;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.TeacherDTOS;
using OnlineClassbook.Entities;
using OnlineClassbook.Exceptions;
using OnlineClassbook.Utils;

namespace OnlineClassbook.Services;

public class TeacherService: ITeacherService
{
    private readonly DataContext _context;

    public TeacherService(DataContext context)
    {
        _context = context;
    }

    public async Task<TeacherToGetDTO> AddTeacher(TeacherToCreateDTO newTeacher)
    {
        Teacher teacher = newTeacher.ToEntity();

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return teacher.ToDto();
    }

    public async Task<List<TeacherToGetDTO>> DeleteTeacherById(int id)
    {
        Teacher teacher = await _context.Teachers
                              .Include(t => t.Address)
                              .FirstOrDefaultAsync(t => t.Id == id) 
                          ?? throw new InvalidIdException($"Teacher with Id '{id}' not found.");

        _context.Teachers.Remove(teacher);

        await _context.SaveChangesAsync();

        return await _context.Teachers
            .Select(t => t.ToDto())
            .ToListAsync();
    }

    public async Task<bool> UpdateOrCreateTeacherAddress(int id, Address newAddress)
    {
        Teacher teacher = await _context.Teachers
                              .Include(t => t.Address)
                              .FirstOrDefaultAsync(t => t.Id == id) 
                          ?? throw new InvalidIdException($"Teacher with Id '{id}' not found.");

        bool created = false;
        if (teacher.Address == null)
        {
            teacher.Address = new Address();
            created = true;
        }
        teacher.Address.City = newAddress.City;
        teacher.Address.Street = newAddress.Street;
        teacher.Address.Number = newAddress.Number;

        await _context.SaveChangesAsync();
        return created;
    }

    public async Task<string> AssignTeacherToCourse(int id, int courseId)
    {
        Teacher foundTeacher = await _context.Teachers
                              .Include(t => t.Address)
                              .FirstOrDefaultAsync(t => t.Id == id) 
                          ?? throw new InvalidIdException($"Teacher with Id '{id}' not found.");
        
        Course foundCourse = await _context.Courses.FindAsync(courseId)
                             ?? throw new InvalidIdException($"Course with Id '{courseId}' not found.");

        foundTeacher.Course = foundCourse ?? throw new InvalidException($"The Teacher with id {id} is already subscribed to this course with id {courseId}.");
        
        await _context.SaveChangesAsync();
        return "Assigned";
    }

    public async Task<TeacherToGetDTO> PromoteTeacher(int id, string teacherNewRank)
    {
        Teacher teacher = await _context.Teachers
                                   .FirstOrDefaultAsync(t => t.Id == id) 
                               ?? throw new InvalidIdException($"Teacher with Id '{id}' not found.");
        teacher.Rank = Enum.Parse<TeacherRank>(teacherNewRank);
        
        await _context.SaveChangesAsync();
        return teacher.ToDto();
    }

    public async Task<List<MarkToGetFromTeacherDTO>> GetTeacherGivenMarks(int teacherId)
    {
        if(!await _context.Teachers.AnyAsync(t => t.Id == teacherId))
        {
            throw new InvalidIdException($"Teacher with Id '{teacherId}' not found.");
        } 

        return await _context.Marks
            .Include(m => m.Course)
            .Where(m => m.TeacherId == teacherId)
            .Select(m => m.ToGivenMarkDto())
            .ToListAsync();
    }
}