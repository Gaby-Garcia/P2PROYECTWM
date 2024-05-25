using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.PerfomanceReviews;

public class Add : PageModel
{
    [BindProperty] public PerfomanceReviewsDto PerfomanceReviewsDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPerfomanceReviewService _service;

    public Add(IPerfomanceReviewService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        PerfomanceReviewsDto = new PerfomanceReviewsDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            PerfomanceReviewsDto = response.Data;
        }

        if (PerfomanceReviewsDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<PerfomanceReviewsDto> response;
        
        response = await _service.SaveAsync(PerfomanceReviewsDto);
        Errors = response.Errors;

        if (Errors.Count > 0)
        {   
            return Page();
        }

        PerfomanceReviewsDto = response.Data;
        return RedirectToPage("./List");
    }
}