using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Pyrolls;

public class Delete : PageModel
{
    [BindProperty] public PayrollsDto PayrollsDto { get; set; }
    
    public Dictionary<int, string> EmployeeName { get; set; }


    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPyrollsService _service;
    private readonly IEmployeeService _employeeService;

    public Delete(IPyrollsService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
        EmployeeName = new Dictionary<int, string>();

    }
    public async Task<IActionResult> OnGet(int id)
    {
        PayrollsDto = new PayrollsDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        PayrollsDto = response.Data;

        var employee = await _employeeService.GetAllAsync();
        if (employee?.Data != null)
        {
            EmployeeName = employee.Data.ToDictionary(d => d.id, d => d.Name);
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (PayrollsDto == null)
        {
            return NotFound();
        }
        var response = await _service.DeleteAsync(PayrollsDto.id);
        return RedirectToPage("./List");
    }
}