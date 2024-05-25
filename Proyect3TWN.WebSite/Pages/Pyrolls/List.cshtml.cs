using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Pyrolls;

public class List : PageModel
{

    private readonly IPyrollsService _service;
    public List<PayrollsDto> pyrolls { get; set; }


    public List(IPyrollsService service)
    {
        pyrolls = new List<PayrollsDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        pyrolls = response.Data;

        return Page();
    }
}