using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Users;

public class Edit : PageModel
{
    [BindProperty] public UsersDto UsersDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IUsersService _service;

    public Edit(IUsersService service)
    {
        _service = service;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        UsersDto = new UsersDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            UsersDto = response.Data;
        }

        if (UsersDto == null)
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

        Response<UsersDto> response;

        if (UsersDto.id > 0)
        {
            //Actualizacion
            response = await _service.UpdateAsync(UsersDto);
        }
        else
        {
            //Insercion
            response = await _service.SaveAsync(UsersDto);
        }

        if (Errors.Count > 0)
        {
            return Page();
        }

        UsersDto = response.Data;
        return RedirectToPage("./List");
    }
}