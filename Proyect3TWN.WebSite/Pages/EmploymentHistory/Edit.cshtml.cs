using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.EmploymentHistory;

public class Edit : PageModel
{
    [BindProperty] public EmploymentHistoryDto EmploymentHistoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IEmploymentHistoryService _service;

    public Edit(IEmploymentHistoryService service)
    {
        _service = service;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        EmploymentHistoryDto = new EmploymentHistoryDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            EmploymentHistoryDto = response.Data;
        }

        if (EmploymentHistoryDto == null)
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

        if (EmploymentHistoryDto.id > 0)
        {
            //Actualizacion
            response = await _service.UpdateAsync(EmploymentHistoryDto);
        }
        else
        {
            //Insercion
            response = await _service.SaveAsync(EmploymentHistoryDto);
        }

        if (Errors.Count > 0)
        {
            return Page();
        }

        EmploymentHistoryDto = response.Data;
        return RedirectToPage("./List");
    }
}