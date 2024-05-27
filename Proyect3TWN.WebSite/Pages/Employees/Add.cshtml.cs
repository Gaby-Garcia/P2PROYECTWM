// En la clase PageModel

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyect3TWN.WebSite.Pages.Employees;

    public class Add : PageModel
    {
        [BindProperty] public EmployeesDto EmployeesDto { get; set; }
        public List<DepartmentDto> Departments { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        private readonly IEmployeeService _service;
        private readonly IDepartmentService _departmentService;

        public Add(IEmployeeService service, IDepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            EmployeesDto = new EmployeesDto();

            var departmentResponse = await _departmentService.GetAllAsync();
            Departments = departmentResponse.Data;


            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                EmployeesDto = response.Data;
            }

            if (EmployeesDto == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar los departamentos si hay errores en la validación del modelo
                var departmentResponse = await _departmentService.GetAllAsync();
                Departments = departmentResponse.Data;
                return Page();
            }

            var response = await _service.SaveAsync(EmployeesDto);

            if (response == null )
            {
                ModelState.AddModelError("", "La respuesta del servicio es nula o no contiene datos válidos.");
                // Recargar los departamentos
                var departmentResponse = await _departmentService.GetAllAsync();
                Departments = departmentResponse.Data;
                return Page();
            }

            if (response.Errors != null && response.Errors.Count > 0)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                // Recargar los departamentos
                var departmentResponse = await _departmentService.GetAllAsync();
                Departments = departmentResponse.Data;
                return Page();
            }

            EmployeesDto = response.Data;
            return RedirectToPage("./List");
        }    }
