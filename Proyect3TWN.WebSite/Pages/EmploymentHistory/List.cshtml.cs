using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class List : PageModel
{

    private readonly IEmploymentHistoryService _service;
    private readonly IEmployeeService _employeeService;

    public List<EmploymentHistoryDto> EmploymentHistory { get; set; }
    public Dictionary<int, string> EmployeeName { get; set; } 

    public List(IEmploymentHistoryService service, IEmployeeService employeeService)
    {
        EmploymentHistory = new List<EmploymentHistoryDto>();
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        EmploymentHistory = response.Data;

        var employee = await _employeeService.GetAllAsync();
        var employees = employee.Data;
        EmployeeName = employees.ToDictionary(d => d.id, d => d.Name);
        
        foreach (var H in EmploymentHistory.ToList())
        {
            if (!EmployeeName.ContainsKey(H.ID_Employee))
            {
                await _service.DeleteAsync(H.id); 
                EmploymentHistory.Remove(H);
            }
        }
        return Page();
    }
}