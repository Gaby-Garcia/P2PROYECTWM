using Microsoft.AspNetCore.Mvc;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Core.Http;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PerfomanceReviewsController : ControllerBase
{
      private readonly IPerfomanceReviewsService _perfomanceReviews;
      private readonly IEmployeesService _employeesService;
    
    public PerfomanceReviewsController( IPerfomanceReviewsService perfomanceReviews, IEmployeesService employeesService)
    {
        _perfomanceReviews = perfomanceReviews;
        _employeesService = employeesService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<PerfomanceReview>>>> GetAllPerfomance()
    {
        var response = new Response<List<PerfomanceReviewsDto>>
        {
            Data = await _perfomanceReviews.GetAllAsyncPR()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<PerfomanceReview>>> PostPerfomance([FromBody] PerfomanceReviewsDto perfomanceReviewsDto)
    {
        
       // Validar campos de tipo string
        if (perfomanceReviewsDto.Comments == "string" || string.IsNullOrWhiteSpace(perfomanceReviewsDto.Comments) || perfomanceReviewsDto.Comments == "0")
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "Comments es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (perfomanceReviewsDto.Goals == "string"|| string.IsNullOrWhiteSpace(perfomanceReviewsDto.Goals) || perfomanceReviewsDto.Goals == "0")
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "Goals es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (perfomanceReviewsDto.Clasification == 0)
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "Clasification  no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        if (perfomanceReviewsDto.id == 0)
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "El ID  no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }

        if (perfomanceReviewsDto.ID_Employee == 0)
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "El ID del Empleado no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }

        //------------
        if (!await _employeesService.EmployeeExist(perfomanceReviewsDto.ID_Employee))
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "El ID del Empleado no existe." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<PerfomanceReviewsDto>()
        {
            Data = await _perfomanceReviews.SaveAsycnPR(perfomanceReviewsDto)
        };
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<PerfomanceReviewsDto>>> GetbyIdPerfomance(int id)
    {
        var response = new Response<PerfomanceReviewsDto>();
        if (!await _perfomanceReviews.PerfomanceReviewsExist(id))
        {
            response.Errors.Add("Perfomance Review not found");
            return NotFound(response);
        }

        response.Data = await _perfomanceReviews.GetByIdPR(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<PerfomanceReview>>> UpdatePerfomance([FromBody] PerfomanceReviewsDto perfomanceReviewsDto)
    {
          
        // Validar campos de tipo string
        if (perfomanceReviewsDto.Comments == "string" || string.IsNullOrWhiteSpace(perfomanceReviewsDto.Comments) || perfomanceReviewsDto.Goals == "0")
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "Comments es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (perfomanceReviewsDto.Goals == "string"|| string.IsNullOrWhiteSpace(perfomanceReviewsDto.Goals) || perfomanceReviewsDto.Goals == "0")
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "Goals es obligatorio." }
            };
            return BadRequest(responseError);
        }

        if (perfomanceReviewsDto.Clasification == 0)
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "Clasification  no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        
        if (perfomanceReviewsDto.id == 0)
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "El ID  no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }

        if (perfomanceReviewsDto.ID_Employee == 0)
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "El ID del Empleado no puede ser igual a 0." }
            };
            return BadRequest(responseError);
        }
        //---
        if (!await _employeesService.EmployeeExist(perfomanceReviewsDto.ID_Employee))
        {
            var responseError = new Response<PerfomanceReview>
            {
                Errors = new List<string> { "El ID del Empleado no existe no existe." }
            };
            return BadRequest(responseError);
        }
        var response = new Response<PerfomanceReviewsDto>();
        if (!await _perfomanceReviews.PerfomanceReviewsExist(perfomanceReviewsDto.id))
        {
            response.Errors.Add("Perfomance Review not found");
            return NotFound(response);
        }
        if (!await _perfomanceReviews.PerfomanceReviewsExist(perfomanceReviewsDto.id))
        {
            response.Errors.Add("Perfomance Reviews not found");
            return NotFound(response);
        }

        response.Data = await _perfomanceReviews.UpdateAsyncPR(perfomanceReviewsDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsyncPerfomance(int id)
    {
        var perfomance = await _perfomanceReviews.DeleteAsyncPR(id);
        var response = new Response<bool>();
        response.Data = perfomance;
        if (perfomance == null)
        {
            response.Errors.Add("Perfomance Review not found");
            return NotFound(response);
        }

        response.Message = ("Perfomance Review found it!");
        return Ok(response);
    }
}