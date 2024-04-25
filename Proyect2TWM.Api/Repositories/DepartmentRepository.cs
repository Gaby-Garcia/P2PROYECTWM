using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;


namespace Proyect2TWM.Api.Repositories;

public class DepartmentRepository : IDepartmentRespository
{
    private readonly IDbContext _dbContext;

    public DepartmentRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Department> SaveAsycnD(Department department)
    {
        department.id = await _dbContext.Connection.InsertAsync(department);
        return department;
    }

    public async Task<Department> UpdateAsyncD(Department department)
    {
        await _dbContext.Connection.UpdateAsync(department);
        return department;
    }

    public async Task<List<Department>> GetAllAsyncD()
    {
        const string sql = "SELECT * FROM Department WHERE isDeleted = 0";
        var departments = await _dbContext.Connection.QueryAsync<Department>(sql);
        return departments.ToList();
    }

    public async Task<bool> DeleteAsyncD(int id)
    {
        var department = await GetByIdD(id);
        if (department == null)
            return false;
        department.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(department);
    }

    public async Task<Department> GetByIdD(int id)
    {
        var department = await _dbContext.Connection.GetAsync<Department>(id);
        if (department == null)
            return null;
        return department.IsDeleted == true ? null : department;
    }
}