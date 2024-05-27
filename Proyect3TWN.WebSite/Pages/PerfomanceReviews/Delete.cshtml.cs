using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.PerfomanceReviews;

public class Delete : PageModel
{
    [BindProperty] public PerfomanceReviewsDto PerfomanceReviewsDto { get; set; }
    
    public Dictionary<int, string> EmployeeName { get; set; }


    public List<string> Errors { get; set; } = new List<string>();
    private readonly IPerfomanceReviewService _service;
    private readonly IEmployeeService _employeeService;

    public Delete(IPerfomanceReviewService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
        EmployeeName = new Dictionary<int, string>();

    }
    public async Task<IActionResult> OnGet(int id)
    {
        PerfomanceReviewsDto = new PerfomanceReviewsDto();

        var response = await _service.GetById(id);
        if (response == null || response.Data == null)
        {
            return RedirectToPage("/Error");
        }
        PerfomanceReviewsDto = response.Data;

        var employee = await _employeeService.GetAllAsync();
        if (employee?.Data != null)
        {
            EmployeeName = employee.Data.ToDictionary(d => d.id, d => d.Name);
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (PerfomanceReviewsDto == null)
        {
            return NotFound();
        }
        var response = await _service.DeleteAsync(PerfomanceReviewsDto.id);
        return RedirectToPage("./List");
    }
}