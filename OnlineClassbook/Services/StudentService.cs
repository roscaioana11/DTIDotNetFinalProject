
using System.Data;
using OnlineClassbook.Data;
using OnlineClassbook.DTOs.CourseDTOS;
using OnlineClassbook.DTOs.MarkDTOS;
using OnlineClassbook.DTOs.StudentDTOS;
using OnlineClassbook.Entities;
using OnlineClassbook.Exceptions;
using OnlineClassbook.Utils;

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
        Student student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id)
                          ?? throw new InvalidIdException($"Student with Id '{id}' not found.");

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
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == id) 
            ?? throw new InvalidIdException($"Student with Id '{id}' not found.");

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        return await _context.Students
            .Select(s => _mapper.Map<StudentToGetDTO>(s))
            .ToListAsync();
    }

    public async Task<List<StudentToGetDTO>> UpdateStudent(StudentToUpdateDTO updateStudent)
    {
        Student student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == updateStudent.Id)
            ?? throw new InvalidIdException($"Student with Id '{updateStudent.Id}' not found.");

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
            .FirstOrDefaultAsync(s => s.Id == studentId)
            ?? throw new InvalidIdException($"Student with Id '{studentId}' not found.");

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
    
    public async Task<List<CourseToGetDTO>> GetAllCourses()
    {
        List<Course> dbCourses = await _context.Courses.ToListAsync();
        return dbCourses.Select(c => c.ToDto()).ToList();
    }

    public async Task<CourseToGetDTO> AddCourse(CourseToCreateDTO courseToCreate)
    {
        Course course = courseToCreate.ToEntity();
        if (_context.Courses.Any(c => c.Name == courseToCreate.Name))
        {
            throw new DuplicatedException($"Course with name: {course.Name} already exists");
        }

        _context.Courses.Add(courseToCreate.ToEntity());
        await _context.SaveChangesAsync();

        return course.ToDto();
    }
    
    public async Task<string> AssignStudentToCourse(int studentId, int courseId)
    {
        Student foundStudent = await _context.Students.Include(c => c.Courses).FirstOrDefaultAsync(s => s.Id == studentId)
           ?? throw new InvalidIdException($"Student with Id '{studentId}' not found.");
        
        Course foundCourse = await _context.Courses.FindAsync(courseId)
           ?? throw new InvalidIdException($"Course with Id '{courseId}' not found.");
        
        if (foundStudent.Courses.Contains(foundCourse))
        {
            throw new InvalidException($"The student with id {studentId} is already subscribed to this course with id {courseId}.");
        }
        
        // foundCourse.Students.Add(foundStudent);
        foundStudent.Courses.Add(foundCourse);
        
        await _context.SaveChangesAsync();
        return "Assigned";
    }

    public async Task<MarkToGetDTO> AddMarks(MarkToCreateDTO markCreate)
    {
        Mark mark = markCreate.ToEntity();
        
        if (await _context.Marks.AnyAsync(x => x.Id == mark.Id))
        {
            throw new DuplicatedException($"Duplicated mark id {mark.Id}");
        }
        
        Student student = await _context.Students.FirstOrDefaultAsync(s => s.Id == mark.StudentId) 
                      ?? throw new InvalidIdException($"Student with Id '{mark.StudentId}' not found.");
        
        Course course = await _context.Courses.Include(s => s.Students).FirstOrDefaultAsync(c => c.Id == mark.CourseId) 
                     ?? throw new InvalidIdException($"Course with Id '{mark.CourseId}' not found.");
        
        
        if (!course.Students.Any(s => s.Id == student.Id))
        {
            course.Students.Add(student);
        }
        
        _context.Marks.Add(mark);
        await _context.SaveChangesAsync();
        
        return mark.ToDto();
    }

    public async Task<List<MarkToGetDTO>> GetMarksByStudentId(int studentId)
    {
        if (!await _context.Students.AnyAsync(s => s.Id == studentId))
        {
            throw new InvalidIdException($"Student with Id '{studentId}' not found.");
        }
        
        return await _context.Marks
            .Include(c => c.Course)
            .Where(s => s.StudentId == studentId)
            .Select(m => m.ToDto())
            .ToListAsync();
    }
    

    public async Task<List<MarkToGetDTO>> GetMarksByCourseIdAndStudentId(int studentId, int courseId)
    {
        if (!await _context.Students.AnyAsync(s => s.Id == studentId))
        {
            throw new InvalidIdException($"Student with Id '{studentId}' not found.");
        }
        
        if (!await _context.Courses.AnyAsync(c => c.Id == courseId))
        {
            throw new InvalidIdException($"Course with Id '{courseId}' not found.");
        }
        
        return await _context.Marks
            .Include(c => c.Course)
            .Where(s => s.StudentId == studentId && s.CourseId == courseId)
            .Select(m => m.ToDto())
            .ToListAsync();
    }

    public async Task<double> GetAverageStudentCourseMarks(int studentId, int courseId)
    {
        if (!await _context.Students.AnyAsync(s => s.Id == studentId))
        {
            throw new InvalidIdException($"Invalid student id {studentId}");
        }

        double averageStudentCourseMarks = await _context.Marks
            .Where(s => s.StudentId == studentId && s.CourseId == courseId)
            .AverageAsync(a=> a.Value);
        return averageStudentCourseMarks;
    }

    public async Task<List<T>> GetStudentOrderByMarksAverage<T>(Func<Student, double, T> select, Sorting sort)
    {
        var studentsWithAverageMarks = await _context.Students
            .Where(s => s.Marks.Any())
            .Select(s => new {
                Student = s,
                AverageMark = s.Marks
                    .GroupBy(mark => mark.CourseId)
                    .Select(marks => marks.Average(mark => mark.Value))
                    .Average()
            }).ToListAsync();
        
        var sortedStudents = sort == Sorting.ASC
            ? studentsWithAverageMarks.OrderBy(sa => sa.AverageMark)
            : studentsWithAverageMarks.OrderByDescending(sa => sa.AverageMark);
        
        return sortedStudents.Select(s => select(s.Student, s.AverageMark)).ToList();
    }

    public async Task<List<CourseToGetDTO>> DeleteCourseById(int courseId)
    {
        Course course = await _context.Courses
                              .FirstOrDefaultAsync(c => c.Id == courseId) 
                          ?? throw new InvalidIdException($"Student with Id '{courseId}' not found.");

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();

        return await _context.Courses
            .Select(c => c.ToDto())
            .ToListAsync();
    }
}