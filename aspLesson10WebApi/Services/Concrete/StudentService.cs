using aspLesson10WebApi.Entities;
using aspLesson10WebApi.Repository.Abstract;
using aspLesson10WebApi.Services.Abstract;
using System.Linq.Expressions;

namespace aspLesson10WebApi.Services.Concrete;
public class StudentService : IStudentService
{
    // private readonly fields for injecting : 
    private readonly IStudentRepository _studentRepository;

    // parametric constructor for injecting :
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    // implemented methods : 
    public async Task AddAsync(Student entity)
        => await _studentRepository.AddAsync(entity);

    public async Task DeleteAsync(Student entity)
        => await _studentRepository.DeleteAsync(entity);    

    public async Task<IEnumerable<Student>> GetAllAsync()
        => await _studentRepository.GetAllAsync();

    public async Task<Student> GetAsync(Expression<Func<Student, bool>> expression)
        => await _studentRepository.GetAsync(expression);

    public async Task UpdateAsync(Student entity)
        => await _studentRepository.UpdateAsync(entity);       
}
