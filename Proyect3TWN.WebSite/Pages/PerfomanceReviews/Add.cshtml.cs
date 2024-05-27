using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.PerfomanceReviews;

public class Add : PageModel
{
    [BindProperty] public PerfomanceReviewsDto PerfomanceReviewsDto { get; set; }
    
    public List<EmployeesDto> Employees { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPerfomanceReviewService _service;
    private readonly IEmployeeService _employeeService;


    public Add(IPerfomanceReviewService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        PerfomanceReviewsDto = new PerfomanceReviewsDto();
        
        var employee = await _employeeService.GetAllAsync();
        Employees = employee.Data;

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            PerfomanceReviewsDto = response.Data;
        }

        if (PerfomanceReviewsDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }

        Response<PerfomanceReviewsDto> response;
        
        response = await _service.SaveAsync(PerfomanceReviewsDto);
        if (response == null || response.Data == null)
        {
            ModelState.AddModelError("", "La respuesta del servicio es nula o no contiene datos válidos.");
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }

       
        if (response.Errors != null && response.Errors.Count > 0)
        {
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError("", error);
            }
            var employee = await _employeeService.GetAllAsync();
            Employees = employee.Data;
            return Page();
        }

        PerfomanceReviewsDto = response.Data;
        return RedirectToPage("./List");
    }
}