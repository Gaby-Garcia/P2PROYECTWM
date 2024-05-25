using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IPerfomanceReviewsRepository
{
    Task<PerfomanceReview> SaveAsycnPerfomance(PerfomanceReview PerfomanceReview);
    
    Task<PerfomanceReview> UpdateAsyncPerfomance(PerfomanceReview perfomanceReview);
    
    Task<List<PerfomanceReview>> GetAllAsyncPerfomance();
    
    Task<bool> DeleteAsyncPerfomance(int id);
    
    Task<PerfomanceReview> GetByIdPerfomance(int id);
    Task<PerfomanceReview> GetByName(string name, int id = 0);
}