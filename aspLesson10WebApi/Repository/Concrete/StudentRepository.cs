using aspLesson10WebApi.Data;
using aspLesson10WebApi.Entities;
using aspLesson10WebApi.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace aspLesson10WebApi.Repository.Concrete;
public class StudentRepository : IStudentRepository
{
    // private fields for injecting : 
    private readonly StudentDBContext _context;

    // parametric constructor for injecting :
    public StudentRepository(StudentDBContext context)
    {
        _context = context;
    }

    // implemented methods from interface : 
    public async Task AddAsync(Student entity)
    {
        await _context.Students.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student entity)
    {
        await Task.Run(() => _context.Students.Remove(entity));
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        // these 2 options has same meaning : 

        // return await _context.Students.ToListAsync();
        return await Task.Run(() => {
            return _context.Students;
        });
    }

    public async Task<Student> GetAsync(Expression<Func<Student, bool>> expression)
    {
        var item = await _context.Students.FirstOrDefaultAsync(expression);
        return item;
    }

    public async Task UpdateAsync(Student entity)
    {
        await Task.Run(()=>_context.Update(entity));
        await _context.SaveChangesAsync();
    }
}
