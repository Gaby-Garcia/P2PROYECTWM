using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Pyrolls;

public class Delete : PageModel
{
    [BindProperty] public PayrollsDto PayrollsDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPyrollsService _service;

    public Delete(IPyrollsService service)
    {
        _service = service;
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

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(PayrollsDto.id);
        return RedirectToPage("./List");
    }
}