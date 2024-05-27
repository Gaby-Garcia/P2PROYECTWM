using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Employees;

public class DeleteModel : PageModel
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    [BindProperty]
    public EmployeesDto EmployeesDto { get; set; }

    public Dictionary<int, string> DepartmentNames { get; set; }

    public DeleteModel(IEmployeeService employeeService, IDepartmentService departmentService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        DepartmentNames = new Dictionary<int, string>();
    }

    public async Task<IActionResult> OnGet(int id)
    {
        var employeeResponse = await _employeeService.GetById(id);
        if (employeeResponse?.Data == null)
        {
            return NotFound();
        }

        EmployeesDto = employeeResponse.Data;

        var departmentResponse = await _departmentService.GetAllAsync();
        if (departmentResponse?.Data != null)
        {
            DepartmentNames = departmentResponse.Data.ToDictionary(d => d.id, d => d.Name);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (EmployeesDto == null)
        {
            return NotFound();
        }

        await _employeeService.DeleteAsync(EmployeesDto.id);
        return RedirectToPage("./List");
    }
}