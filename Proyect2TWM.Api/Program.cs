using Dapper.Contrib.Extensions;
using Proyect2TWM.Api.DataAccess;
using Proyect2TWM.Api.Repositories;
using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Services;
using Proyect2TWM.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IEmployeesService, EmployeeService>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

builder.Services.AddScoped<IDepartmentRespository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IEmploymentHistoryRepository, EmploymentHistoryRepository>();
builder.Services.AddScoped<IEmploymentHistoryService, EmploymentHistoryService>();

builder.Services.AddScoped<IPayrollsRepository, PayrollsRepository>();
builder.Services.AddScoped<IPyrollsService, PyrollsService>();

builder.Services.AddScoped<IPerfomanceReviewsRepository, PerfomanceReviewsRepository>();
builder.Services.AddScoped<IPerfomanceReviewsService, PerfomanceReviewsService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<IVacationsAbsencesRepository, VacationsAbsencesRepository>();
builder.Services.AddScoped<IVacationsAbsencesService, VacationsAbsencesServices>();
builder.Services.AddScoped<IDbContext, DbContext>();

var app = builder.Build();
//mapeo
SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("Proyect2TWM.Core.Entities."))
        name = name.Replace("Proyect2TWM.Core.Entities.", "");
    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};


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