using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class Delete : PageModel
{
    [BindProperty] public VacationsAbsencesDto VacationsAbsencesDto { get; set; }

    
    public Dictionary<int, string> EmployeesName { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    private readonly IVacationsAbsencesService _service;
    private readonly IEmployeeService _employeeService;

    public Delete(IVacationsAbsencesService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
        EmployeesName = new Dictionary<int, string>();

    }
    public async Task<IActionResult> OnGet(int id)
    {
        VacationsAbsencesDto = new VacationsAbsencesDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        VacationsAbsencesDto = response.Data;

        var employeeResponse = await _employeeService.GetAllAsync();
        if (employeeResponse?.Data != null)
        {
            EmployeesName = employeeResponse.Data.ToDictionary(d => d.id, d => d.Name);
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (VacationsAbsencesDto == null)
        {
            return NotFound();
        }
        var response = await _service.DeleteAsync(VacationsAbsencesDto.id);
        return RedirectToPage("./List");
    }
}