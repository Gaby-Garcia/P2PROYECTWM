using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
      private readonly IEmployeesService _employeesService;
      private readonly IDepartmentService _departmentService;
      private static readonly HashSet<string> AllowedGenders = new HashSet<string> { "Hombre", "Mujer", "No binario" };

      private string _gender;

    public EmployeesController( IEmployeesService employeesService, IDepartmentService departmentService)
    {
        _employeesService = employeesService;
        _departmentService = departmentService;
    }
    private bool IsValidEmailFormat(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@(gmail|hotmail|outlook)\.com$";
        return Regex.IsMatch(email, emailPattern);
    }
    
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        string phonePattern = @"^\d{10}$";
        return Regex.IsMatch(phoneNumber, phonePattern);
    }
    [HttpGet]
    public async Task<ActionResult<Response<List<Employee>>>> GetAllEmployees()
    {
        var response = new Response<List<EmployeesDto>>
        {
            Data = await _employeesService.GetAllAsyncE()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Employee>>> PostEmployees([FromBody] EmployeesDto employeesDto)
    {
       
        // Validar campos de tipo string
        if (employeesDto.Name == "string" || string.IsNullOrWhiteSpace(employeesDto.Name) || employeesDto.Name == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El nombre es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.LastName == "string"|| string.IsNullOrWhiteSpace(employeesDto.LastName) || employeesDto.LastName == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El apellido es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.Address == "string" || string.IsNullOrWhiteSpace(employeesDto.Address) || employeesDto.Address == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "La dirección es obligatoria." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.Phone == "string" || string.IsNullOrWhiteSpace(employeesDto.Phone) || employeesDto.Phone == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El teléfono es obligatorio." }
            };
            return BadRequest(responseError);
        }
        if (!IsValidPhoneNumber(employeesDto.Phone))
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El número de teléfono debe tener exactamente 10 dígitos." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.Email == "string" || !IsValidEmailFormat(employeesDto.Email) || employeesDto.Email == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El correo electrónico no tiene un formato válido." }
            };
            return BadRequest(responseError);
        }


        if (employeesDto.ID_Department == 0)
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El ID del departamento no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        // Validar el género proporcionado
        if (!AllowedGenders.Contains(employeesDto.Gender))
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El género proporcionado no es válido." }
            };
            return BadRequest(responseError);
        }
    
        // Validar si el departamento existe
        if (!await _departmentService.DepartmentExist(employeesDto.ID_Department))
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El ID del Departamento no existe." }
            };
            return BadRequest(responseError);
        }

        var response = new Response<EmployeesDto>();

        if (await _employeesService.ExistByName(employeesDto.Name))
        {
            response.Errors.Add($"Employee Name {employeesDto.Name} already exist");
            return BadRequest(response);
        }

        response.Data = await _employeesService.SaveAsycnE(employeesDto);
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<EmployeesDto>>> GetbyIdEmployees(int id)
    {
        var response = new Response<EmployeesDto>();
        if (!await _employeesService.EmployeeExist(id))
        {
            response.Errors.Add("Employee not found");
            return NotFound(response);
        }

        response.Data = await _employeesService.GetByIdE(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Employee>>> UpdateEmployees([FromBody] EmployeesDto employeesDto)
    {
        
          // Validar campos de tipo string
        if (employeesDto.Name == "string" || string.IsNullOrWhiteSpace(employeesDto.Name) || employeesDto.Name == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El nombre es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.LastName == "string"|| string.IsNullOrWhiteSpace(employeesDto.LastName) || employeesDto.LastName == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El apellido es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.Address == "string" || string.IsNullOrWhiteSpace(employeesDto.Address) || employeesDto.Address == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "La dirección es obligatoria." }
            };
            return BadRequest(responseError);
        }

        if (employeesDto.Phone == "string" || string.IsNullOrWhiteSpace(employeesDto.Phone) || employeesDto.Phone == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El teléfono es obligatorio." }
            };
            return BadRequest(responseError);
        }

        // Validar el correo electrónico
        if (employeesDto.Email == "string" || !IsValidEmailFormat(employeesDto.Email) || employeesDto.Email == "0")
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El correo electrónico no tiene un formato válido o no pertenece a un dominio permitido." }
            };
            return BadRequest(responseError);
        }
        if (employeesDto.ID_Department == 0)
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El ID del departamento no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        // Validar el género proporcionado
        if (!AllowedGenders.Contains(employeesDto.Gender))
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El género proporcionado no es válido." }
            };
            return BadRequest(responseError);
        }
    
        // Validar si el departamento existe
        if (!await _departmentService.DepartmentExist(employeesDto.ID_Department))
        {
            var responseError = new Response<Employee>
            {
                Errors = new List<string> { "El ID del Departamento no existe." }
            };
            return BadRequest(responseError);
        }
//--
        var response = new Response<EmployeesDto>();
        if (!await _employeesService.EmployeeExist(employeesDto.id))
        {
            response.Errors.Add("Employee not found");
            return NotFound(response);
        }
        
        if (await _employeesService.ExistByName(employeesDto.Name, employeesDto.id))
        {
            response.Errors.Add($"Employee Name {employeesDto.Name} already exist");
            return BadRequest(response);
        }

        response.Data = await _employeesService.UpdateAsyncE(employeesDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsyncEmployees(int id)
    {
        var employee = await _employeesService.DeleteAsyncE(id);
        var response = new Response<bool>();
        response.Data = employee;
        if (employee == null)
        {
            response.Errors.Add("Employee not found");
            return NotFound(response);
        }

        response.Message = ("Employee found it!");
        return Ok(response);
    }
}