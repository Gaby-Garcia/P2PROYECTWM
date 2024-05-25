using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class Add : PageModel
{
    [BindProperty] public EmploymentHistoryDto EmploymentHistory { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmploymentHistoryService _service;

    public Add(IEmploymentHistoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        EmploymentHistory = new EmploymentHistoryDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            EmploymentHistory = response.Data;
        }

        if (EmploymentHistory == null)
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

        Response<EmploymentHistoryDto> response;
        
        response = await _service.SaveAsync(EmploymentHistory);
        Errors = response.Errors;

        if (Errors.Count > 0)
        {   
            return Page();
        }

        EmploymentHistory = response.Data;
        return RedirectToPage("./List");
    }
}