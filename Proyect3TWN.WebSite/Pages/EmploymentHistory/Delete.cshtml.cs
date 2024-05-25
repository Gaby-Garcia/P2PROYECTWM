using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class Delete : PageModel
{
    [BindProperty] public EmploymentHistoryDto EmploymentHistoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmploymentHistoryService _service;

    public Delete(IEmploymentHistoryService service)
    {
        _service = service;
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

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(EmploymentHistoryDto.id);
        return RedirectToPage("./List");
    }
}