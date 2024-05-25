using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Users;

public class Delete : PageModel
{
    [BindProperty] public UsersDto UsersDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IUsersService _service;

    public Delete(IUsersService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        UsersDto = new UsersDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        UsersDto = response.Data;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(UsersDto.id);
        return RedirectToPage("./List");
    }
}