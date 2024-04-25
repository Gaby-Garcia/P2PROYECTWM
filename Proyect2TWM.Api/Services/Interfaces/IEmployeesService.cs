using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IEmployeesService
{
    Task<bool> EmployeeExist(int id);

    Task<List<EmployeesDto>> GetAllAsyncE();


    Task<EmployeesDto> SaveAsycnE(EmployeesDto employeesDto);

    Task<EmployeesDto> GetByIdE(int id);

    Task<EmployeesDto> UpdateAsyncE(EmployeesDto employeesDto);

    Task<bool> DeleteAsyncE(int id);
}