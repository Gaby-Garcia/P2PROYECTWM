using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class DepartmentDto : DtoBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Supervisor { get; set; }

    public DepartmentDto()
    {
        
    }
    public DepartmentDto(Department department)
    {
        id = department.id;
        Name = department.Name;
        Description = department.Description;
        Supervisor = department.Supervisor;
    }
}