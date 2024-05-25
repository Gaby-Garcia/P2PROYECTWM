using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VacationsAbsencesController : ControllerBase
{
      private readonly IVacationsAbsencesService _vacationsAbsences;
      private readonly IEmployeesService _employeesService;
    
    public VacationsAbsencesController( IVacationsAbsencesService vacationsAbsences, IEmployeesService employeesService)
    {
        _vacationsAbsences = vacationsAbsences;
        _employeesService = employeesService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<VacationsAbsences>>>> GetAllVA()
    {
        var response = new Response<List<VacationsAbsencesDto>>
        {
            Data = await _vacationsAbsences.GetAllAsyncVA()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<VacationsAbsences>>> PostVA([FromBody] VacationsAbsencesDto vacationsAbsencesDto)
    {
         // Validar campos de tipo string
        if (vacationsAbsencesDto.Type == "string" || string.IsNullOrWhiteSpace(vacationsAbsencesDto.Type) || vacationsAbsencesDto.Type == "0")
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "Type es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (vacationsAbsencesDto.Deducted == "string"|| string.IsNullOrWhiteSpace(vacationsAbsencesDto.Deducted) || vacationsAbsencesDto.Deducted == "0")
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "Deducted es obligatorio." }
            };
            return BadRequest(responseError);
        }


        if (vacationsAbsencesDto.ID_Employee == 0)
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "El ID de Empleado no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        //---
        if (!await _employeesService.EmployeeExist(vacationsAbsencesDto.ID_Employee))
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "El ID del Empleado no existe no existe." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<VacationsAbsencesDto>()
        {
            Data = await _vacationsAbsences.SaveAsycnVA(vacationsAbsencesDto)
        };
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<VacationsAbsencesDto>>> GetbyIdVA(int id)
    {
        var response = new Response<VacationsAbsencesDto>();
        if (!await _vacationsAbsences.VacationsAbsencesExist(id))
        {
            response.Errors.Add("Vacations or Absences not found");
            return NotFound(response);
        }

        response.Data = await _vacationsAbsences.GetByIdVA(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<VacationsAbsences>>> UpdateVA([FromBody] VacationsAbsencesDto vacationsAbsencesDto)
    {
        // Validar campos de tipo string
        if (vacationsAbsencesDto.Type == "string" || string.IsNullOrWhiteSpace(vacationsAbsencesDto.Type) || vacationsAbsencesDto.Deducted == "0")
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "Type es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (vacationsAbsencesDto.Deducted == "string"|| string.IsNullOrWhiteSpace(vacationsAbsencesDto.Deducted) || vacationsAbsencesDto.Deducted == "0")
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "Deducted es obligatorio." }
            };
            return BadRequest(responseError);
        }


        if (vacationsAbsencesDto.ID_Employee == 0)
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "El ID de Empleado no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }

        //--
        if (!await _employeesService.EmployeeExist(vacationsAbsencesDto.ID_Employee))
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "El ID del Empleado no existe no existe." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<VacationsAbsencesDto>();
        if (!await _vacationsAbsences.VacationsAbsencesExist(vacationsAbsencesDto.id))
        {
            response.Errors.Add("Vacations or Absences not found");
            return NotFound(response);
        }
        response.Data = await _vacationsAbsences.UpdateAsyncVA(vacationsAbsencesDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsyncVA(int id)
    {
        var vacations = await _vacationsAbsences.DeleteAsyncVA(id);
        var response = new Response<bool>();
        response.Data = vacations;
        if (vacations == null)
        {
            response.Errors.Add("Vacations or Absences not found");
            return NotFound(response);
        }

        response.Message = ("Vacations or Absences found it!");
        return Ok(response);
    }
}