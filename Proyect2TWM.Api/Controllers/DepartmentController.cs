using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
     private readonly IDepartmentService _departmentService;
    
    
    public DepartmentController( IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Department>>>> GetAll()
    {
        var response = new Response<List<DepartmentDto>>
        {
            Data = await _departmentService.GetAllAsyncD()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Department>>> Post([FromBody] DepartmentDto departmentDto)
    {
        
        // Validar campos de tipo string
        if (departmentDto.Name == "string" || string.IsNullOrWhiteSpace(departmentDto.Name) || departmentDto.Name == "0")
        {
            var responseError = new Response<Department>
            {
                Errors = new List<string> { "El Nombre es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (departmentDto.Description == "string"|| string.IsNullOrWhiteSpace(departmentDto.Description) || departmentDto.Description == "0")
        {
            var responseError = new Response<Department>
            {
                Errors = new List<string> { "Descripcion es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (departmentDto.Supervisor == "string" || string.IsNullOrWhiteSpace(departmentDto.Supervisor) || departmentDto.Supervisor == "0")
        {
            var responseError = new Response<Department>
            {
                Errors = new List<string> { "Supervisor es obligatorio" }
            };
            return BadRequest(responseError);
        }
        
        if (departmentDto.id == 0)
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El ID de Department no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        //--
        var response = new Response<DepartmentDto>()
        {
            Data = await _departmentService.SaveAsycnD(departmentDto)
        };
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<DepartmentDto>>> GetbyId(int id)
    {
        var response = new Response<DepartmentDto>();
        if (!await _departmentService.DepartmentExist(id))
        {
            response.Errors.Add("Department not found");
            return NotFound(response);
        }

        response.Data = await _departmentService.GetByIdD(id);
        return Ok(response);
    }

    [HttpPut]

    public async Task<ActionResult<Response<DepartmentDto>>> Update([FromBody] DepartmentDto departmentDto)
    {
            
        // Validar campos de tipo string
        if (departmentDto.Name == "string" || string.IsNullOrWhiteSpace(departmentDto.Name) || departmentDto.Name == "0")
        {
            var responseError = new Response<Department>
            {
                Errors = new List<string> { "El Nombre es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (departmentDto.Description == "string"|| string.IsNullOrWhiteSpace(departmentDto.Description)  || departmentDto.Description == "0")
        {
            var responseError = new Response<Department>
            {
                Errors = new List<string> { "Descripcion es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (departmentDto.Supervisor == "string" || string.IsNullOrWhiteSpace(departmentDto.Supervisor)  || departmentDto.Supervisor == "0")
        {
            var responseError = new Response<Department>
            {
                Errors = new List<string> { "Supervisor es obligatorio" }
            };
            return BadRequest(responseError);
        }
        
        if (departmentDto.id == 0)
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El ID de Department no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        //---
        var response = new Response<DepartmentDto>();
        if (!await _departmentService.DepartmentExist(departmentDto.id))
        {
            response.Errors.Add("Department not found");
            return NotFound(response);
        }

        response.Data = await _departmentService.UpdateAsyncD(departmentDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAync(int id)
    {
        var department = await _departmentService.DeleteAsyncD(id);
        var response = new Response<bool>();
        response.Data = department;
        if (department == null)
        {
            response.Errors.Add("Department not found");
            return NotFound(response);
        }

        response.Message = ("Department found it!");
        return Ok(response);
    }
    
}