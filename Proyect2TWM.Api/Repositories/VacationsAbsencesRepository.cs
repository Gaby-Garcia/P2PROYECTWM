using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;

namespace Proyect2TWM.Api.Repositories;

public class VacationsAbsencesRepository : IVacationsAbsencesRepository
{
    //Se prepara la clase para saber que se estara trabajando con una base de datos 
    private readonly IDbContext _dbContext;

    public VacationsAbsencesRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<VacationsAbsences> SaveAsycnVA(VacationsAbsences vacationsAbsences)
    {
        vacationsAbsences.id = await _dbContext.Connection.InsertAsync(vacationsAbsences);
        return vacationsAbsences;
    }

    public async Task<VacationsAbsences> UpdateAsyncVA(VacationsAbsences vacationsAbsences)
    {
        await _dbContext.Connection.UpdateAsync(vacationsAbsences);
        return vacationsAbsences;
    }

    public async Task<List<VacationsAbsences>> GetAllAsyncVA()
    {
        const string sql = "SELECT * FROM VacationsAbsences WHERE isDeleted = 0";
        var vacationsAbsences = await _dbContext.Connection.QueryAsync<VacationsAbsences>(sql);
        return vacationsAbsences.ToList();
    }

    public async Task<bool> DeleteAsyncVA(int id)
    {
        var vacationsAbsences = await GetByIdVA(id);
        if (vacationsAbsences == null)
            return false;
        vacationsAbsences.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(vacationsAbsences);
    }

    public async Task<VacationsAbsences> GetByIdVA(int id)
    {
        var vacationsAbsences = await _dbContext.Connection.GetAsync<VacationsAbsences>(id);
        if (vacationsAbsences == null)
            return null;
        return vacationsAbsences.IsDeleted == true ? null : vacationsAbsences;
    }
}