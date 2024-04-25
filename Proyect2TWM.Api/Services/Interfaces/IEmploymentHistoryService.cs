using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IEmploymentHistoryService
{
    Task<bool> EmploymentHistoryExist(int id);

    Task<List<EmploymentHistoryDto>> GetAllAsyncEH();


    Task<EmploymentHistoryDto> SaveAsycnEH(EmploymentHistoryDto employmentHistory);

    Task<EmploymentHistoryDto> GetByIdEH(int id);

    Task<EmploymentHistoryDto> UpdateAsyncEH(EmploymentHistoryDto employmentHistory);

    Task<bool> DeleteAsyncEH(int id);
}