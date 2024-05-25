using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Pyrolls;

public class Edit : PageModel
{
    [BindProperty] public PayrollsDto PayrollsDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPyrollsService _service;

    public Edit(IPyrollsService service)
    {
        _service = service;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        PayrollsDto = new PayrollsDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            PayrollsDto = response.Data;
        }

        if (PayrollsDto == null)
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

        Response<PayrollsDto> response;

        if (PayrollsDto.id > 0)
        {
            //Actualizacion
            response = await _service.UpdateAsync(PayrollsDto);
        }
        else
        {
            //Insercion
            response = await _service.SaveAsync(PayrollsDto);
        }

        if (Errors.Count > 0)
        {
            return Page();
        }

        PayrollsDto = response.Data;
        return RedirectToPage("./List");
    }
}