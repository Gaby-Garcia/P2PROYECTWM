using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Employees;

public class List : PageModel
{
   
    private readonly IEmployeeService _service;
    public List<EmployeesDto> employee { get; set; }


    public List(IEmployeeService service)
    {
        employee = new List<EmployeesDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        employee = response.Data;

        return Page();
    }
}