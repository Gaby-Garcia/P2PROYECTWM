using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Department;

public class ListModel : PageModel
{

    private readonly IDepartmentService _service;
    public List<DepartmentDto> department { get; set; }


    public ListModel(IDepartmentService service)
    {
        department = new List<DepartmentDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        department = response.Data;

        return Page();
    }
}