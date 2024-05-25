using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Users;

public class List : PageModel
{

    private readonly IUsersService _service;
    public List<UsersDto> user { get; set; }


    public List(IUsersService service)
    {
        user = new List<UsersDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        user = response.Data;

        return Page();
    }
}