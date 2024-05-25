using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IPyrollsService
{
    Task<Response<List<PayrollsDto>>> GetAllAsync();

    Task<Response<PayrollsDto>> GetById(int id);

    Task<Response<PayrollsDto>> SaveAsync(PayrollsDto payrollsDto);

    Task<Response<PayrollsDto>> UpdateAsync(PayrollsDto payrollsDto);

    Task<Response<bool>> DeleteAsync(int id);
}