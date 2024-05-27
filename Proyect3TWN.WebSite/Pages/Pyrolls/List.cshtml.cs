using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Pyrolls;

public class List : PageModel
{

    private readonly IPyrollsService _service;
    private readonly IEmployeeService _employeeService;
    public List<PayrollsDto> pyrolls { get; set; }
    public Dictionary<int, string> Employees { get; set; } 



    public List(IPyrollsService service, IEmployeeService employeeService)
    {
        pyrolls = new List<PayrollsDto>();
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        pyrolls = response.Data;

        var employeeResponse = await _employeeService.GetAllAsync();
        var employees = employeeResponse.Data;
        Employees = employees.ToDictionary(d => d.id, d => d.Name);
        
        foreach (var emp in pyrolls.ToList())
        {
            if (!Employees.ContainsKey(emp.ID_Employee))
            {
                await _service.DeleteAsync(emp.id);
                pyrolls.Remove(emp);
            }
        }
        
        return Page();
    }
}