using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class List : PageModel
{

    private readonly IEmploymentHistoryService _service;
    public List<EmploymentHistoryDto> EmploymentHistory { get; set; }


    public List(IEmploymentHistoryService service)
    {
        EmploymentHistory = new List<EmploymentHistoryDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        EmploymentHistory = response.Data;

        return Page();
    }
}