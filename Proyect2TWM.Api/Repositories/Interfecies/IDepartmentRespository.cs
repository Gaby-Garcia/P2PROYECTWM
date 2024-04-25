using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IDepartmentRespository
{
    Task<Department> SaveAsycnD(Department department);
    
    Task<Department> UpdateAsyncD(Department department);
    
    Task<List<Department>> GetAllAsyncD();
    
    Task<bool> DeleteAsyncD(int id);
    
    Task<Department> GetByIdD(int id);
    
}