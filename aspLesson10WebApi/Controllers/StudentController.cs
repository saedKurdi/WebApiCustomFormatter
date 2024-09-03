using aspLesson10WebApi.DTO;
using aspLesson10WebApi.Entities;
using aspLesson10WebApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace aspLesson10WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    // private fields for injecting : 
    private readonly IStudentService _studentService;

    // parametric constructor for injecting :
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/<StudentController>
    [HttpGet]
    public async Task<IEnumerable<StudentDTO>> Get()
    {
        var items = await _studentService.GetAllAsync();
        var dto = items.Select(s => {
            return new StudentDTO
            {
                Id = s.Id,
                Score = s.Score,
                SeriaNO = s.SeriaNO,
                Age = s.Age,
                FullName = s.FullName,
            };
        });
        return dto;
    }

    // GET api/<StudentController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _studentService.GetAsync(s => s.Id == id);
        if (item == null) return NotFound();
        var dto = new StudentDTO
        {
            Id = item.Id,
            Score = item.Score,
            SeriaNO = item.SeriaNO,
            Age = item.Age,
            FullName = item.FullName,
        };
        return Ok(dto);
    }

    // POST api/<StudentController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StudentAddDTO dto)
    {
        var entity = new Student
        {
            Age = dto.Age,
            FullName = dto.FullName,
            Score = dto.Score,
            SeriaNO = dto.SeriaNO,
        };
        await _studentService.AddAsync(entity);
        return Ok(dto);
    }

    // PUT api/<StudentController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] StudentAddDTO dto)
    {
        var entity = await _studentService.GetAsync(s=>s.Id == id);   
        if (entity == null) return NotFound("Student was not found !");
        entity.Age = dto.Age;
        entity.FullName = dto.FullName;
        entity.Score = dto.Score;
        entity.SeriaNO = dto.SeriaNO;
        await _studentService.UpdateAsync(entity);
        return Ok(dto);
    }

    // DELETE api/<StudentController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _studentService.GetAsync(s => s.Id == id);
        if (entity == null) return NotFound("Student was not found !");
        await _studentService.DeleteAsync(entity);
        return NoContent();
    }
}
