using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class Edit : PageModel
{
    [BindProperty] public VacationsAbsencesDto VacationsAbsencesDto { get; set; }

    public List<EmployeesDto> Employees { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IVacationsAbsencesService _service;
    private readonly IEmployeeService _employeeService;

    public Edit(IVacationsAbsencesService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        VacationsAbsencesDto = new VacationsAbsencesDto();

        var employeeResponse = await _employeeService.GetAllAsync();
        Employees = employeeResponse.Data;
        
        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            VacationsAbsencesDto = response.Data;
        }

        if (VacationsAbsencesDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var employeeResponse = await _employeeService.GetAllAsync();
            Employees = employeeResponse.Data;
            return Page();
        }

        Response<VacationsAbsencesDto> response;

        if (VacationsAbsencesDto.id > 0)
        {
            //Actualizacion
            response = await _service.UpdateAsync(VacationsAbsencesDto);
            Errors = response.Errors;

        }
        else
        {
            //Insercion
            response = await _service.SaveAsync(VacationsAbsencesDto);
        }
        if (response == null || response.Data == null)
        {
            ModelState.AddModelError("", "La respuesta del servicio es nula o no contiene datos válidos.");
            var employeeResponse = await _employeeService.GetAllAsync();
            Employees = employeeResponse.Data;
            return Page();
        }
        if (response.Errors != null && response.Errors.Count > 0)
        {
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError("", error);
            }
            var employeeResponse = await _employeeService.GetAllAsync();
            Employees = employeeResponse.Data;
            return Page();
        }

        VacationsAbsencesDto = response.Data;
        return RedirectToPage("./List");
    }
}