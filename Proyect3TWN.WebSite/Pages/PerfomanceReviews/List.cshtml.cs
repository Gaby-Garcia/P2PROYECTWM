using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.PerfomanceReviews;

public class List : PageModel
{
    private readonly IPerfomanceReviewService _service;
    private readonly IEmployeeService _employeeService;
    
    public List<PerfomanceReviewsDto> PerfomanceReview { get; set; }

    public Dictionary<int, string> EmployeeName { get; set; } 
    public List(IPerfomanceReviewService service, IEmployeeService employeeService)
    {
        PerfomanceReview = new List<PerfomanceReviewsDto>();
        _service = service;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        PerfomanceReview = response.Data;

        var employee = await _employeeService.GetAllAsync();
        var employees = employee.Data;
        EmployeeName = employees.ToDictionary(d => d.id, d => d.Name);
        
        foreach (var emp in PerfomanceReview.ToList())
        {
            if (!EmployeeName.ContainsKey(emp.ID_Employee))
            {
                await _service.DeleteAsync(emp.id);
                PerfomanceReview.Remove(emp);
            }
        }
        
        return Page();
    }
}