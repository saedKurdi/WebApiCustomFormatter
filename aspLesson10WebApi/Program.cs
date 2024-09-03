using aspLesson10WebApi.Data;
using aspLesson10WebApi.Formatters.InputFormatters;
using aspLesson10WebApi.Formatters.OutputFormatters;
using aspLesson10WebApi.Repository.Abstract;
using aspLesson10WebApi.Repository.Concrete;
using aspLesson10WebApi.Services.Abstract;
using aspLesson10WebApi.Services.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // adding output formatters : 
    options.OutputFormatters.Insert(0,new VCardOutputFormatter());
    options.OutputFormatters.Insert(1, new TextCsvOutputFormatter());

    // adding input formatters : 
    options.InputFormatters.Insert(0, new VCardInputFormatter());
    options.InputFormatters.Insert(1,new TextCsvInputFormatter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding - injecting db : 
var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<StudentDBContext>(opt => opt.UseSqlServer(conn));

// registering injecting repoistories : 
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// registerin injecting services : 
builder.Services.AddScoped<IStudentService,StudentService>();

// building app : 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
