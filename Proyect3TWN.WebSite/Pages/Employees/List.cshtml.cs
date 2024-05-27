using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Employees;

public class List : PageModel
{
   
    private readonly IEmployeeService _service;
    private readonly IDepartmentService _departmentService;
    public List<EmployeesDto> employee { get; set; }
    public Dictionary<int, string> DepartmentNames { get; set; } 


    public List(IEmployeeService service, IDepartmentService departmentService)
    {
        employee = new List<EmployeesDto>();
        _service = service;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        employee = response.Data;
        
        var departmentResponse = await _departmentService.GetAllAsync();
        var departments = departmentResponse.Data;
        DepartmentNames = departments.ToDictionary(d => d.id, d => d.Name);
        
        foreach (var emp in employee.ToList())
        {
            if (!DepartmentNames.ContainsKey(emp.ID_Department))
            {
                await _service.DeleteAsync(emp.id);
                employee.Remove(emp);
            }
        }
        return Page();
    }
}