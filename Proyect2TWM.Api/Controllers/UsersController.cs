using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;
namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
      private readonly IUsersService _usersService;
    
    
    public UsersController( IUsersService usersService)
    {
        _usersService = usersService;
    }

    private bool IsValidEmailFormat(string email)
    {
        // Expresión regular para validar el formato del correo electrónico
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@(gmail|hotmail|outlook)\.com$";

        // Validar el formato del correo electrónico usando expresiones regulares
        return Regex.IsMatch(email, emailPattern);
    }
    
    [HttpGet]
    public async Task<ActionResult<Response<List<Users>>>> GetAllUsers()
    {
        var response = new Response<List<UsersDto>>
        {
            Data = await _usersService.GetAllAsyncU()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Users>>> PostUsers([FromBody] UsersDto usersDto)
    {
          // Validar campos de tipo string
        if (usersDto.UserName == "string" || string.IsNullOrWhiteSpace(usersDto.UserName) || usersDto.UserName == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El nombre de usuario es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (usersDto.Password == "string"|| string.IsNullOrWhiteSpace(usersDto.Password) || usersDto.Password == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "La contraseña es obligatoria." }
            };
            return BadRequest(responseError);
        }
        
        // Validar el correo electrónico
        if (usersDto.Email == "string" || !IsValidEmailFormat(usersDto.Email) || usersDto.Email == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El correo electrónico no tiene un formato válido o no pertenece a un dominio permitido." }
            };
            return BadRequest(responseError);
        }
        if (usersDto.id == 0)
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El ID del Usuario no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        var response = new Response<UsersDto>()
        {
            Data = await _usersService.SaveAsycnU(usersDto)
        };
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<UsersDto>>> GetbyIdUsers(int id)
    {
        var response = new Response<UsersDto>();
        if (!await _usersService.UsersExist(id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Data = await _usersService.GetByIdU(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Users>>> UpdateUsers([FromBody] UsersDto usersDto)
    {
        
        // Validar campos de tipo string
        if (usersDto.UserName == "string" || string.IsNullOrWhiteSpace(usersDto.UserName) || usersDto.Email == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El nombre de usuario es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (usersDto.Password == "string"|| string.IsNullOrWhiteSpace(usersDto.Password) || usersDto.Email == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "La contraseña es obligatoria." }
            };
            return BadRequest(responseError);
        }
        
        // Validar el correo electrónico
        if (usersDto.Email == "string" || !IsValidEmailFormat(usersDto.Email) || usersDto.Email == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El correo electrónico no tiene un formato válido o no pertenece a un dominio permitido." }
            };
            return BadRequest(responseError);
        }
        if (usersDto.id == 0)
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El ID del Usuario no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<UsersDto>();
        if (!await _usersService.UsersExist(usersDto.id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Data = await _usersService.UpdateAsyncU(usersDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsyncUsers(int id)
    {
        var user = await _usersService.DeleteAsyncU(id);
        var response = new Response<bool>();
        response.Data = user;
        if (user == null)
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Message = ("User found it!");
        return Ok(response);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] UsersDto usersDto)
    {
        var isAuthenticated = await _usersService.AuthenticateAsync(usersDto.Email, usersDto.Password, usersDto.UserName);

        // Validar campos de tipo string
        if (usersDto.UserName == "string" || string.IsNullOrWhiteSpace(usersDto.UserName) || usersDto.UserName == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El nombre de usuario es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (usersDto.Password == "string"|| string.IsNullOrWhiteSpace(usersDto.Password) || usersDto.Password == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "La contraseña es obligatoria." }
            };
            return BadRequest(responseError);
        }
        
        // Validar el correo electrónico
        if (usersDto.Email == "string" || !IsValidEmailFormat(usersDto.Email) || usersDto.Email == "0")
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El correo electrónico no tiene un formato válido o no pertenece a un dominio permitido." }
            };
            return BadRequest(responseError);
        }
        if (usersDto.id == 0)
        {
            var responseError = new Response<Users>
            {
                Errors = new List<string> { "El ID del Usuario no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        //--
        var response = new Response<UsersDto>();
        if (!await _usersService.UsersExist(usersDto.id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }
        
        if (isAuthenticated)
        {
            return Ok("Inicio de sesión exitoso");
        }
        else
        {
            return BadRequest("Correo electrónico,  contraseña o Username  incorrectos ");
        }
    }
  

}