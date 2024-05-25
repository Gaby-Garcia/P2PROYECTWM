using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EmploymentHistoryController : ControllerBase
{
      private readonly IEmploymentHistoryService _employmentHistory;
      private readonly IEmployeesService _employeesService;

    
    
    public EmploymentHistoryController( IEmploymentHistoryService employmentHistory, IEmployeesService employeesService)
    {
        _employmentHistory= employmentHistory;
        _employeesService = employeesService;

    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Employment_History>>>> GetAllEH()
    {
        var response = new Response<List<EmploymentHistoryDto>>
        {
            Data = await _employmentHistory.GetAllAsyncEH()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Employment_History>>> PostEH([FromBody] EmploymentHistoryDto employmentHistoryDto)
    {
        
        // Validar campos de tipo string
        if (employmentHistoryDto.CompanyName == "string" || string.IsNullOrWhiteSpace(employmentHistoryDto.CompanyName) || employmentHistoryDto.CompanyName == "0")
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El Nombre de la Compañia es obligatorio." }
            };
            return BadRequest(responseError);
        }

            if (employmentHistoryDto.CompanyPosition == "string"|| string.IsNullOrWhiteSpace(employmentHistoryDto.CompanyPosition) || employmentHistoryDto.CompanyPosition == "0")
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "CompanyPosition es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employmentHistoryDto.Description == "string" || string.IsNullOrWhiteSpace(employmentHistoryDto.Description) || employmentHistoryDto.Description == "0")
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "La descripcion es obligatoria." }
            };
            return BadRequest(responseError);
        }

        if (employmentHistoryDto.ID_Employee <= 0 )
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El ID del Empleado no puede ser igual a 0 o nulo" }
            };
            return BadRequest(responseError);
        }

        //-----------------
        if (!await _employeesService.EmployeeExist(employmentHistoryDto.ID_Employee))
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "El ID del Empleado no existe no existe." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<EmploymentHistoryDto>();

        if (await _employmentHistory.ExistByCompanyName(employmentHistoryDto.CompanyName))
        {
            response.Errors.Add($"CompanyName {employmentHistoryDto.CompanyName} already exist");
            return BadRequest(response);
        }

        response.Data = await _employmentHistory.SaveAsycnEH(employmentHistoryDto);
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<EmploymentHistoryDto>>> GetbyIdEH(int id)
    {
        var response = new Response<EmploymentHistoryDto>();
        if (!await _employmentHistory.EmploymentHistoryExist(id))
        {
            response.Errors.Add("Employment History not found");
            return NotFound(response);
        }

        response.Data = await _employmentHistory.GetByIdEH(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Employment_History>>> UpdateEH([FromBody] EmploymentHistoryDto employmentHistoryDto)
    {
        
        
        // Validar campos de tipo string
        if (employmentHistoryDto.CompanyName == "string" || string.IsNullOrWhiteSpace(employmentHistoryDto.CompanyName)|| employmentHistoryDto.CompanyName == "0")
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El Nombre de la Compañia es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employmentHistoryDto.CompanyPosition == "string"|| string.IsNullOrWhiteSpace(employmentHistoryDto.CompanyPosition) || employmentHistoryDto.CompanyPosition == "0")
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "CompanyPosition es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (employmentHistoryDto.Description == "string" || string.IsNullOrWhiteSpace(employmentHistoryDto.Description) || employmentHistoryDto.Description == "0")
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "La descripcion es obligatoria." }
            };
            return BadRequest(responseError);
        }

        if (employmentHistoryDto.ID_Employee <= 0 )
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El ID del Empleado no puede ser igual a 0 o nulo" }
            };
            return BadRequest(responseError);
        }
        
        //---------------
        var response = new Response<EmploymentHistoryDto>();
        if (!await _employeesService.EmployeeExist(employmentHistoryDto.ID_Employee))
        {
            var responseError = new Response<Employment_History>
            {
                Errors = new List<string> { "El ID del Empleado no existe." }
            };
            return BadRequest(responseError);
        }
        
        if (!await _employmentHistory.EmploymentHistoryExist(employmentHistoryDto.id))
        {
            response.Errors.Add("Employment History not found");
            return NotFound(response);
        }

        response.Data = await _employmentHistory.UpdateAsyncEH(employmentHistoryDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsyncEH(int id)
    {
        var employment = await _employmentHistory.DeleteAsyncEH(id);
        var response = new Response<bool>();
        response.Data = employment;
        if (employment == null)
        {
            response.Errors.Add("Employment History not found");
            return NotFound(response);
        }

        response.Message = ("Employment History found it!");
        return Ok(response);
    }
}