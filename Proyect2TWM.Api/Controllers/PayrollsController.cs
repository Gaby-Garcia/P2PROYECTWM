using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PayrollsController : ControllerBase
{
      private readonly IPyrollsService _pyrollsService;
      private readonly IEmployeesService _employeesService;
    
    public PayrollsController( IPyrollsService pyrollsService, IEmployeesService employeesService)
    {
        _pyrollsService = pyrollsService;
        _employeesService = employeesService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Pyrolls>>>> GetAllP()
    {
        var response = new Response<List<PayrollsDto>>
        {
            Data = await _pyrollsService.GetAllAsyncP()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Pyrolls>>> PostP([FromBody] PayrollsDto payrollsDto)
    {
        if (payrollsDto.Salary == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "Salary no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        if (payrollsDto.Bonuses == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "Bonuses no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.Deductions == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "Deductions no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.HoursWorked == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "HoursWorked no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.ID_Employee == 0)
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "El ID de Empleado no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.id == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "El ID  no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        //--
        if (!await _employeesService.EmployeeExist(payrollsDto.ID_Employee))
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "El ID del Empleado no existe no existe." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<PayrollsDto>()
        
        {
            Data = await _pyrollsService.SaveAsycnP(payrollsDto)
        };
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<PayrollsDto>>> GetbyIdP(int id)
    {
        var response = new Response<PayrollsDto>();
        if (!await _pyrollsService.PyrollsExist(id))
        {
            response.Errors.Add("Pyroll not found");
            return NotFound(response);
        }

        response.Data = await _pyrollsService.GetByIdP(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Pyrolls>>> UpdateP([FromBody] PayrollsDto payrollsDto)
    {
        
        if (payrollsDto.Salary == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "Salary no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        if (payrollsDto.Bonuses == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "Bonuses no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.Deductions == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "Deductions no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.HoursWorked == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "HoursWorked no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.ID_Employee == 0)
        {
            var responseError = new Response<VacationsAbsences>
            {
                Errors = new List<string> { "El ID de Empleado no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        if (payrollsDto.id == 0)
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "El ID  no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        //--
        var response = new Response<PayrollsDto>();
       
        if (!await _employeesService.EmployeeExist(payrollsDto.ID_Employee))
        {
            var responseError = new Response<Pyrolls>
            {
                Errors = new List<string> { "El ID del Empleado no existe no existe." }
            };
            return BadRequest(responseError);
        }
        
        if (!await _pyrollsService.PyrollsExist(payrollsDto.id))
        {
            response.Errors.Add("Pyroll not found");
            return NotFound(response);
        }

        response.Data = await _pyrollsService.UpdateAsyncP(payrollsDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsynP(int id)
    {
        var pyroll = await _pyrollsService.DeleteAsyncP(id);
        var response = new Response<bool>();
        response.Data = pyroll;
        if (pyroll == null)
        {
            response.Errors.Add("Pyroll not found");
            return NotFound(response);
        }

        response.Message = ("Pyroll found it!");
        return Ok(response);
    }
}