using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class Delete : PageModel
{
    [BindProperty] public VacationsAbsencesDto VacationsAbsencesDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IVacationsAbsencesService _service;

    public Delete(IVacationsAbsencesService service)
    {
        _service = service;
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

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(VacationsAbsencesDto.id);
        return RedirectToPage("./List");
    }
}