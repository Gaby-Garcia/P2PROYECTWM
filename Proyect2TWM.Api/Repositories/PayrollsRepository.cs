using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;


namespace Proyect2TWM.Api.Repositories;

public class PayrollsRepository : IPayrollsRepository
{
    //Se prepara la clase para saber que se estara trabajando con una base de datos 
    private readonly IDbContext _dbContext;

    public PayrollsRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Pyrolls> SaveAsycnP(Pyrolls pyrolls)
    {
        pyrolls.id = await _dbContext.Connection.InsertAsync(pyrolls);
        return pyrolls;
    }

    public async Task<Pyrolls> UpdateAsyncP(Pyrolls pyrolls)
    {
        await _dbContext.Connection.UpdateAsync(pyrolls);
        return pyrolls;
    }

    public async Task<List<Pyrolls>> GetAllAsyncP()
    {
        const string sql = "SELECT * FROM Pyrolls WHERE isDeleted = 0";
        var pyrolls = await _dbContext.Connection.QueryAsync<Pyrolls>(sql);
        return pyrolls.ToList();
    }

    public async Task<bool> DeleteAsyncP(int id)
    {
        var pyrolls = await GetByIdP(id);
        if (pyrolls == null)
            return false;
        pyrolls.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(pyrolls);
    }

    public async Task<Pyrolls> GetByIdP(int id)
    {
        var pyrolls = await _dbContext.Connection.GetAsync<Pyrolls>(id);
        if (pyrolls == null)
            return null;
        return pyrolls.IsDeleted == true ? null : pyrolls;
    }
}