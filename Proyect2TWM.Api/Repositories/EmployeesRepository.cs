using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;


namespace Proyect2TWM.Api.Repositories.Interfecies;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly IDbContext _dbContext;

    public EmployeesRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Employee> SaveAsycnEmployee(Employee employee)
    {
        employee.id = await _dbContext.Connection.InsertAsync(employee);
        return employee;
    }

    public async Task<Employee> UpdateAsyncEmployee(Employee employee)
    {
        await _dbContext.Connection.UpdateAsync(employee);
        return employee;
    }

    public async Task<List<Employee>> GetAllAsyncEmployee()
    {
        const string sql = "SELECT * FROM Employee WHERE isDeleted = 0";
        var empployees = await _dbContext.Connection.QueryAsync<Employee>(sql);
        return empployees.ToList();
    }

    public async Task<bool> DeleteAsyncEmployee(int id)
    {
        var employee = await GetByIdEmployee(id);
        if (employee == null)
            return false;
        employee.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(employee);
    }

    public async Task<Employee> GetByIdEmployee(int id)
    {
        var employee = await _dbContext.Connection.GetAsync<Employee>(id);
        if (employee == null)
            return null;
        return employee.IsDeleted == true ? null : employee;
    }
    
    public  async Task<Employee> GetByName(string name, int id = 0)
    {
        string sql = $"SELECT * FROM Employee WHERE Name = '{name}' AND id <> {id}";
        var employees = await _dbContext.Connection.QueryAsync<Employee>(sql);
        return employees.ToList().FirstOrDefault();
    }

}