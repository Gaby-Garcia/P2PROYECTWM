using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Department;

public class Delete : PageModel
{
    [BindProperty] public DepartmentDto DepartmentDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IDepartmentService _service;

    public Delete(IDepartmentService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        DepartmentDto = new DepartmentDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        DepartmentDto = response.Data;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(DepartmentDto.id);
        return RedirectToPage("./List");
    }

}