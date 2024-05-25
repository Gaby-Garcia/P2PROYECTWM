using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Department;

public class Add : PageModel
{
    [BindProperty] public DepartmentDto DepartmentDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IDepartmentService _service;

    public Add(IDepartmentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        DepartmentDto = new DepartmentDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            DepartmentDto = response.Data;
        }

        if (DepartmentDto == null)
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

        Response<DepartmentDto> response;
        
        response = await _service.SaveAsync(DepartmentDto);
        Errors = response.Errors;

        if (Errors.Count > 0)
        {   
            return Page();
        }

        DepartmentDto = response.Data;
        return RedirectToPage("./List");
    }
}