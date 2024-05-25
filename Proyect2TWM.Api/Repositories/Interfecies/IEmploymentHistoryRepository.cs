using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IEmploymentHistoryRepository
{
    Task<Employment_History> SaveAsycnEH(Employment_History employmentHistory);
    
    Task<Employment_History> UpdateAsyncEH(Employment_History employmentHistory);
    
    Task<List<Employment_History>> GetAllAsyncEH();
    
    Task<bool> DeleteAsyncEH(int id);
    
    Task<Employment_History> GetByIdEH(int id);
    
    Task<Employment_History> GetByCompanyName(string companyName, int id = 0);
}