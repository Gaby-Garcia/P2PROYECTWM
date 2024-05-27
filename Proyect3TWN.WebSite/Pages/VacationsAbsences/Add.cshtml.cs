using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class Add : PageModel
{
    [BindProperty] public VacationsAbsencesDto VacationsAbsencesDto { get; set; }

    public List<EmployeesDto> Employees { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IVacationsAbsencesService _service;
    private readonly IEmployeeService _employeeService;

    public Add(IVacationsAbsencesService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        VacationsAbsencesDto = new VacationsAbsencesDto();

        var employeesResponse = await _employeeService.GetAllAsync();
        Employees = employeesResponse.Data;
        
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
            var employeesResponse = await _employeeService.GetAllAsync();
            Employees = employeesResponse.Data;
            return Page();
        }

        Response<VacationsAbsencesDto> response;
        
        response = await _service.SaveAsync(VacationsAbsencesDto);
        if (response == null || response.Data == null)
        {
            ModelState.AddModelError("", "La respuesta del servicio es nula o no contiene datos válidos.");
            var employeesResponse = await _employeeService.GetAllAsync();
            Employees = employeesResponse.Data;
            return Page();
        }

       
        if (response.Errors != null && response.Errors.Count > 0)
        {
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError("", error);
            }
            var employeesResponse = await _employeeService.GetAllAsync();
            Employees = employeesResponse.Data;
            return Page();
        }

        VacationsAbsencesDto = response.Data;
        return RedirectToPage("./List");
    }
}