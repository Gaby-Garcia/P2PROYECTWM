using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;


namespace Proyect2TWM.Api.Repositories.Interfecies;

public class PerfomanceReviewsRepository : IPerfomanceReviewsRepository
{
    //Se prepara la clase para saber que se estara trabajando con una base de datos 
    private readonly IDbContext _dbContext;

    public PerfomanceReviewsRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<PerfomanceReview> SaveAsycnPerfomance(PerfomanceReview perfomanceReview)
    {
        perfomanceReview.id = await _dbContext.Connection.InsertAsync(perfomanceReview);
        return perfomanceReview;
    }

    public async Task<PerfomanceReview> UpdateAsyncPerfomance(PerfomanceReview perfomanceReview)
    {
        await _dbContext.Connection.UpdateAsync(perfomanceReview);
        return perfomanceReview;
    }

    public async Task<List<PerfomanceReview>> GetAllAsyncPerfomance()
    {
        const string sql = "SELECT * FROM PerfomanceReview WHERE isDeleted = 0";
        var perfomanceReviews = await _dbContext.Connection.QueryAsync<PerfomanceReview>(sql);
        return perfomanceReviews.ToList();
    }

    public async Task<bool> DeleteAsyncPerfomance(int id)
    {
        var perfomanceReviews = await GetByIdPerfomance(id);
        if (perfomanceReviews == null)
            return false;
        perfomanceReviews.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(perfomanceReviews);
    }

    public async Task<PerfomanceReview> GetByIdPerfomance(int id)
    {
        var perfomanceReviews = await _dbContext.Connection.GetAsync<PerfomanceReview>(id);
        if (perfomanceReviews == null)
            return null;
        return perfomanceReviews.IsDeleted == true ? null : perfomanceReviews;
    }
}