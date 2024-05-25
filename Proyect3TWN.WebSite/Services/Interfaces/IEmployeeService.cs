using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IEmployeeService
{
    Task<Response<List<EmployeesDto>>> GetAllAsync();

    Task<Response<EmployeesDto>> GetById(int id);

    Task<Response<EmployeesDto>> SaveAsync(EmployeesDto employeesDto);

    Task<Response<EmployeesDto>> UpdateAsync(EmployeesDto employeesDto);

    Task<Response<bool>> DeleteAsync(int id);
}