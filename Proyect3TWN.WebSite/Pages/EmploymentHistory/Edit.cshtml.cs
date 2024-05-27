using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class Edit : PageModel
{
    [BindProperty] public EmploymentHistoryDto EmploymentHistoryDto { get; set; }

    public List<EmployeesDto> Employees { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmploymentHistoryService _service;
    private readonly IEmployeeService _employeeService;

    public Edit(IEmploymentHistoryService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        EmploymentHistoryDto = new EmploymentHistoryDto();

        var employee = await _employeeService.GetAllAsync();
        Employees = employee.Data;
        
        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            EmploymentHistoryDto = response.Data;
        }

        if (EmploymentHistoryDto == null)
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

        Response<EmploymentHistoryDto> response;

        if (EmploymentHistoryDto.id > 0)
        {
            //Actualizacion
            response = await _service.UpdateAsync(EmploymentHistoryDto);
            Errors = response.Errors;

        }
        else
        {
            //Insercion
            response = await _service.SaveAsync(EmploymentHistoryDto);
        }
        if (response == null || response.Data == null)
        {
            // Maneja el caso donde la respuesta es nula
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
            // Recargar los departamentos
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }
        EmploymentHistoryDto = response.Data;
        return RedirectToPage("./List");
    }
}