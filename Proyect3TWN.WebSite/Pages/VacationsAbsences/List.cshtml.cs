using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class List : PageModel
{

    private readonly IVacationsAbsencesService _service;
    public List<VacationsAbsencesDto> vacations { get; set; }


    public List(IVacationsAbsencesService service)
    {
        vacations = new List<VacationsAbsencesDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        vacations = response.Data;

        return Page();
    }
}