
using OnlineClassbook.Data;
using OnlineClassbook.DTOs;
using OnlineClassbook.Entities;

namespace OnlineClassbook.Services;

public class StudentService: IStudentService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    
    public StudentService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<StudentToGetDTO>> GetAllStudents()
    {
        List<Student> dbStudents = await _context.Students.ToListAsync();
        return dbStudents.Select(s => _mapper.Map<StudentToGetDTO>(s)).ToList();
    }

    public async Task<StudentToGetDTO> GetStudentById(int id)
    {
        Student student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        
        if (student is null)
            throw new Exception($"Student with Id '{id}' not found.");

        return _mapper.Map<StudentToGetDTO>(student);
    }

    public async Task<List<StudentToGetDTO>> AddStudent(StudentToCreateDTO newStudent)
    {
        Student student = _mapper.Map<Student>(newStudent);

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return await _context.Students
            .Select(s => _mapper.Map<StudentToGetDTO>(s))
            .ToListAsync();
    }

    public async Task<List<StudentToGetDTO>> DeleteStudentById(int id)
    {
        Student student = await _context.Students
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (student is null)
            throw new Exception($"Student with Id '{id}' not found.");

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        return await _context.Students
            .Select(s => _mapper.Map<StudentToGetDTO>(s))
            .ToListAsync();
    }

    public async Task<List<StudentToGetDTO>> UpdateStudent(StudentToUpdateDTO updateStudent)
    {
        Student student = await _context.Students
            .FirstOrDefaultAsync(c => c.Id == updateStudent.Id);
        
        if (student is null)
            throw new Exception($"Student with Id '{updateStudent.Id}' not found.");

        student.LastName = updateStudent.LastName;
        student.FirstName = updateStudent.FirstName;
        student.Age = updateStudent.Age;
        
        await _context.SaveChangesAsync();
        
        return await _context.Students
            .Select(s => _mapper.Map<StudentToGetDTO>(s))
            .ToListAsync();
    }

    public async Task<bool> UpdateOrCreateStudentAddress(int studentId, Address newAddress)
    {
        Student student = await _context.Students
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == studentId);
        
        if (student == null)
        {
            throw new Exception($"Student with Id '{studentId}' not found.");
        }

        bool created = false;
        if (student.Address == null)
        {
            student.Address = new Address();
            created = true;
        }
        student.Address.City = newAddress.City;
        student.Address.Street = newAddress.Street;
        student.Address.Number = newAddress.Number;

        await _context.SaveChangesAsync();
        return created;
    }
}