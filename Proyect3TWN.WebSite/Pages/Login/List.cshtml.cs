using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.Login;

public class List : PageModel
{
    private readonly IUsersService _usersService;

    public List(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [BindProperty]
    public LoginDto LoginDto { get; set; }

    public Response<LoginDto> Response { get; set; }

    public void OnGet()
    {
        LoginDto = new LoginDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Llama al servicio de autenticación
        var response = await _usersService.AuthenticateAsync(LoginDto.Email, LoginDto.Password);
            
        if (response != null && response.Data != null)
        {
            // Redirigir al usuario a la página de inicio después de un inicio de sesión exitoso
            return RedirectToPage("/Index");
        }
        else
        {
            // Manejar errores de autenticación
            Response = new Response<LoginDto>
            {
                Errors = new List<string> { "Correo electrónico o contraseña incorrectos." }
            };
            // Manejar autenticación fallida
            ViewData["ErrorMessage"] = "Invalid login attempt.";
            return Page();
        }
    }
}


