using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRespository _departmentRespository;

    public DepartmentService(IDepartmentRespository departmentRespository)
    {
        _departmentRespository = departmentRespository;
    }

    public async Task<bool> DepartmentExist(int id)
    {
        var department = await _departmentRespository.GetByIdD(id);
        return (department != null);
    }

    public async Task<DepartmentDto> SaveAsycnD(DepartmentDto departmentDto)
    {
        var department = new Department
        {
            Name = departmentDto.Name,
            Description = departmentDto.Description,
            Supervisor = departmentDto.Supervisor,
            CreatedBy = "Gaby-Garcia",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Gaby-Garcia",
            UpdatedDate = DateTime.Now
        };
        department = await _departmentRespository.SaveAsycnD(department);
        departmentDto.id = department.id;

        return departmentDto;
    }

    public async Task<DepartmentDto> UpdateAsyncD(DepartmentDto departmentDto)
    {
        var department = await _departmentRespository.GetByIdD(departmentDto.id);
        if (department == null)
            throw new Exception("Department Not Found");

        department.Name = departmentDto.Name;
        department.Description = departmentDto.Description;
        department.Supervisor = departmentDto.Supervisor;
        department.UpdatedBy = "Gaby-Garcia";
        department.UpdatedDate = DateTime.Now;
        
        await _departmentRespository.UpdateAsyncD(department);
        return departmentDto;
    }

    public async Task<List<DepartmentDto>> GetAllAsyncD()
    {
        var department = await _departmentRespository.GetAllAsyncD();
        var departmentDto = department.Select(c => new DepartmentDto(c)).ToList();
        return departmentDto;
    }

    public async Task<bool> DeleteAsyncD(int id)
    {
        return await _departmentRespository.DeleteAsyncD(id);
    }

    public async Task<DepartmentDto> GetByIdD(int id)
    {
        var department = await _departmentRespository.GetByIdD(id);
        if (department == null)
            throw new Exception("Department not found");

        var departmentDto = new DepartmentDto(department);
        return departmentDto;
    }
}