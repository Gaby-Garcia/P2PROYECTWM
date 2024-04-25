using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IEmployeesRepository
{
    Task<Employee> SaveAsycnEmployee(Employee employee);
    
    Task<Employee> UpdateAsyncEmployee(Employee employee);
    
    Task<List<Employee>> GetAllAsyncEmployee();
    
    Task<bool> DeleteAsyncEmployee(int id);
    
    Task<Employee> GetByIdEmployee(int id);
}