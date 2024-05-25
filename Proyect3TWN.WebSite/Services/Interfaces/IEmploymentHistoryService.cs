using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IEmploymentHistoryService
{
    Task<Response<List<EmploymentHistoryDto>>> GetAllAsync();

    Task<Response<EmploymentHistoryDto>> GetById(int id);

    Task<Response<EmploymentHistoryDto>> SaveAsync(EmploymentHistoryDto employmentHistoryDto);

    Task<Response<EmploymentHistoryDto>> UpdateAsync(EmploymentHistoryDto employmentHistoryDto);

    Task<Response<bool>> DeleteAsync(int id);
}