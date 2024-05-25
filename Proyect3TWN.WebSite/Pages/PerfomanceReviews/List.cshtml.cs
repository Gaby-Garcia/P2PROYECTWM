using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.PerfomanceReviews;

public class List : PageModel
{
    private readonly IPerfomanceReviewService _service;
    public List<PerfomanceReviewsDto> PerfomanceReview { get; set; }


    public List(IPerfomanceReviewService service)
    {
        PerfomanceReview = new List<PerfomanceReviewsDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        PerfomanceReview = response.Data;

        return Page();
    }
}