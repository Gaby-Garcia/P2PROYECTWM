using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Dto;

public class EmployeesDto : DtoBase
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    
    public int ID_Department { get; set; }

   
    public EmployeesDto()
    {

    }

    public EmployeesDto(Employee employee)
    {
        id = employee.id;
        Name = employee.Name;
        LastName = employee.LastName;
        BirthDate = employee.BirthDate;
        Gender = employee.Gender;
        Address = employee.Address;
        Phone = employee.Phone;
        Email = employee.Email;
        ID_Department = employee.ID_Department;

    }
}