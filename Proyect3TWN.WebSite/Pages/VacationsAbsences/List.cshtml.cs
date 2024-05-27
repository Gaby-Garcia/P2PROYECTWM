using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class List : PageModel
{

    private readonly IVacationsAbsencesService _service;
    private readonly IEmployeeService _employeeService;
    
    public Dictionary<int, string> EmployeeName { get; set; } 

    public List<VacationsAbsencesDto> vacations { get; set; }
    
    public List(IVacationsAbsencesService service, IEmployeeService employeeService)
    {
        vacations = new List<VacationsAbsencesDto>();
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        vacations = response.Data;

        var employeeResponse = await _employeeService.GetAllAsync();
        var employees = employeeResponse.Data;
        EmployeeName = employees.ToDictionary(d => d.id, d => d.Name);
        
        foreach (var emp in vacations.ToList())
        {
            if (!EmployeeName.ContainsKey(emp.ID_Employee))
            {
                await _service.DeleteAsync(emp.id);
                vacations.Remove(emp);
            }
        }
        
        return Page();
    }
}