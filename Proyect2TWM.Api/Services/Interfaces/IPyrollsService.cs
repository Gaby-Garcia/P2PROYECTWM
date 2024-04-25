using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IPyrollsService
{
    Task<bool> PyrollsExist(int id);

    Task<List<PayrollsDto>> GetAllAsyncP();


    Task<PayrollsDto> SaveAsycnP(PayrollsDto pyrolls);

    Task<PayrollsDto> GetByIdP(int id);

    Task<PayrollsDto> UpdateAsyncP(PayrollsDto payrolls);

    Task<bool> DeleteAsyncP(int id);
}