using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;


namespace Proyect2TWM.Api.Repositories.Interfecies;

public class EmploymentHistoryRepository : IEmploymentHistoryRepository
{
    //Se prepara la clase para saber que se estara trabajando con una base de datos 
    private readonly IDbContext _dbContext;

    public EmploymentHistoryRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Employment_History> SaveAsycnEH(Employment_History employmentHistory)
    {
        employmentHistory.id = await _dbContext.Connection.InsertAsync(employmentHistory);
        return employmentHistory;
    }

    public async Task<Employment_History> UpdateAsyncEH(Employment_History employmentHistory)
    {
        await _dbContext.Connection.UpdateAsync(employmentHistory);
        return employmentHistory;
    }

    public async Task<List<Employment_History>> GetAllAsyncEH()
    {
        const string sql = "SELECT * FROM Employment_History WHERE isDeleted = 0";
        var employmentHistories = await _dbContext.Connection.QueryAsync<Employment_History>(sql);
        return employmentHistories.ToList();
    }

    public async Task<bool> DeleteAsyncEH(int id)
    {
        var employmentHistory = await GetByIdEH(id);
        if (employmentHistory == null)
            return false;
        employmentHistory.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(employmentHistory);
    }

    public async Task<Employment_History> GetByIdEH(int id)
    {
        var employmentHistory = await _dbContext.Connection.GetAsync<Employment_History>(id);
        if (employmentHistory == null)
            return null;
        return employmentHistory.IsDeleted == true ? null : employmentHistory;
    }
    
}