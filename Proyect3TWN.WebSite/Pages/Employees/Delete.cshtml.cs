using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Employees;

public class Delete : PageModel
{
    [BindProperty] public EmployeesDto EmployeesDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmployeeService _service;

    public Delete(IEmployeeService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        EmployeesDto = new EmployeesDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        EmployeesDto = response.Data;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(EmployeesDto.id);
        return RedirectToPage("./List");
    }
}