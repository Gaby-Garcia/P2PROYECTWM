using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IDepartmentService
{
    Task<Response<List<DepartmentDto>>> GetAllAsync();

    Task<Response<DepartmentDto>> GetById(int id);

    Task<Response<DepartmentDto>> SaveAsync(DepartmentDto departmentDto);

    Task<Response<DepartmentDto>> UpdateAsync(DepartmentDto departmentDto);

    Task<Response<bool>> DeleteAsync(int id);
}