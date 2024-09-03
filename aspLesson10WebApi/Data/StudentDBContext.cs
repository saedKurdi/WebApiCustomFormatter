using aspLesson10WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspLesson10WebApi.Data;
public class StudentDBContext : DbContext
{
    // parametric constructor for injecting from program.cs side : 
    public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options) { }

    // tables which will be replaced in db : 
    public DbSet<Student> Students { get; set; }
}
