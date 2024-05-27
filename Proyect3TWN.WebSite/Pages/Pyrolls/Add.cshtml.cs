using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Pyrolls;

public class Add : PageModel
{
    [BindProperty] public PayrollsDto PayrollsDto { get; set; }

    public List<EmployeesDto> Employees { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPyrollsService _service;
    private readonly IEmployeeService _employeeService;

    public Add(IPyrollsService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        PayrollsDto = new PayrollsDto();
        
        var employee = await _employeeService.GetAllAsync();
        Employees = employee.Data;

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            PayrollsDto = response.Data;
        }

        if (PayrollsDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }

        Response<PayrollsDto> response;
        
        response = await _service.SaveAsync(PayrollsDto);
        if (response == null || response.Data == null)
        {
            ModelState.AddModelError("", "La respuesta del servicio es nula o no contiene datos válidos.");
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }
        
        if (response.Errors != null && response.Errors.Count > 0)
        {
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError("", error);
            }
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }

        PayrollsDto = response.Data;
        return RedirectToPage("./List");
    }
}