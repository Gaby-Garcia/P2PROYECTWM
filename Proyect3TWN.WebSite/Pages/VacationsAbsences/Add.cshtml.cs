﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Pages.VacationsAbsences;

public class Add : PageModel
{
    [BindProperty] public VacationsAbsencesDto VacationsAbsencesDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IVacationsAbsencesService _service;

    public Add(IVacationsAbsencesService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        VacationsAbsencesDto = new VacationsAbsencesDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            VacationsAbsencesDto = response.Data;
        }

        if (VacationsAbsencesDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<VacationsAbsencesDto> response;
        
        response = await _service.SaveAsync(VacationsAbsencesDto);
        Errors = response.Errors;

        if (Errors.Count > 0)
        {   
            return Page();
        }

        VacationsAbsencesDto = response.Data;
        return RedirectToPage("./List");
    }
}