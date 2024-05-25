using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.PerfomanceReviews;

public class Delete : PageModel
{
    [BindProperty] public PerfomanceReviewsDto PerfomanceReviewsDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPerfomanceReviewService _service;

    public Delete(IPerfomanceReviewService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        PerfomanceReviewsDto = new PerfomanceReviewsDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        PerfomanceReviewsDto = response.Data;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(PerfomanceReviewsDto.id);
        return RedirectToPage("./List");
    }
}