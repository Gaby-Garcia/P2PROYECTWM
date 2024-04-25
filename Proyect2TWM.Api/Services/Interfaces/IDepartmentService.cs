using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IDepartmentService
{
    Task<bool> DepartmentExist(int id);

    Task<List<DepartmentDto>> GetAllAsyncD();


    Task<DepartmentDto> SaveAsycnD(DepartmentDto department);

    Task<DepartmentDto> GetByIdD(int id);

    Task<DepartmentDto> UpdateAsyncD(DepartmentDto department);

    Task<bool> DeleteAsyncD(int id);
}