using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Employees;

public class Add : PageModel
{
    [BindProperty] public EmployeesDto EmployeesDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmployeeService _service;

    public Add(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        EmployeesDto = new EmployeesDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            EmployeesDto = response.Data;
        }

        if (EmployeesDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<EmployeesDto> response;
        
        response = await _service.SaveAsync(EmployeesDto);
        Errors = response.Errors;

        if (Errors.Count > 0)
        {   
            return Page();
        }

        EmployeesDto = response.Data;
        return RedirectToPage("./List");
    }
}