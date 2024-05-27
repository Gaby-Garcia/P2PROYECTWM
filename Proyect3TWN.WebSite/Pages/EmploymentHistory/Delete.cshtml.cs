using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class Delete : PageModel
{
    [BindProperty]
    public EmploymentHistoryDto EmploymentHistoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmploymentHistoryService _service;
    private readonly IEmployeeService _employeeService;
    public Dictionary<int, string> EmployeeName { get; set; }

    public Delete(IEmploymentHistoryService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
        EmployeeName = new Dictionary<int, string>();

    }
    public async Task<IActionResult> OnGet(int id)
    {
        EmploymentHistoryDto = new EmploymentHistoryDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        EmploymentHistoryDto = response.Data;

        // Obtener la lista de departamentos
        var employee = await _employeeService.GetAllAsync();
        if (employee?.Data != null)
        {
            EmployeeName = employee.Data.ToDictionary(d => d.id, d => d.Name);
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (EmploymentHistoryDto == null)
        {
            return NotFound();
        }
        var response = await _service.DeleteAsync(EmploymentHistoryDto.id);
        return RedirectToPage("./List");
    }
}