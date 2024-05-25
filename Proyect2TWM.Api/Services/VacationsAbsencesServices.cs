using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class VacationsAbsencesServices : IVacationsAbsencesService
{
    private readonly IVacationsAbsencesRepository _vacationsAbsences;

    public VacationsAbsencesServices(IVacationsAbsencesRepository vacationsAbsences)
    {
        _vacationsAbsences = vacationsAbsences;
    }

    public async Task<bool> VacationsAbsencesExist(int id)
    {
        var vacations = await _vacationsAbsences.GetByIdVA(id);
        return (vacations != null);
    }

    public async Task<VacationsAbsencesDto> SaveAsycnVA(VacationsAbsencesDto vacationsAbsencesDto)
    {
        var vacations = new VacationsAbsences
        {
            ID_Employee = vacationsAbsencesDto.ID_Employee,
            Type = vacationsAbsencesDto.Type,
            Deducted = vacationsAbsencesDto.Deducted,
            StartDate = vacationsAbsencesDto.StartDate,
            EndDate = vacationsAbsencesDto.EndDate,
            CreatedBy = "Gaby-Garcia",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Gaby-Garcia",
            UpdatedDate = DateTime.Now
        };
        vacations = await _vacationsAbsences.SaveAsycnVA(vacations);
        vacationsAbsencesDto.id = vacations.id;

        return vacationsAbsencesDto;
    }

    public async Task<VacationsAbsencesDto> UpdateAsyncVA(VacationsAbsencesDto vacationsAbsencesDto)
    {
        var vacations = await _vacationsAbsences.GetByIdVA(vacationsAbsencesDto.id);
        if (vacations == null)
            throw new Exception("Vacations or Absences Not Found");
        
        vacations.ID_Employee = vacationsAbsencesDto.ID_Employee;
        vacations.Type = vacationsAbsencesDto.Type;
        vacations.Deducted = vacationsAbsencesDto.Deducted;
        vacations.StartDate = vacationsAbsencesDto.StartDate;
        vacations.EndDate = vacationsAbsencesDto.EndDate;
        vacations.UpdatedBy = "Gaby-Garcia";
        vacations.UpdatedDate = DateTime.Now;
        
        await _vacationsAbsences.UpdateAsyncVA(vacations);
        return vacationsAbsencesDto;
    }

    public async Task<List<VacationsAbsencesDto>> GetAllAsyncVA()
    {
        var vacations = await _vacationsAbsences.GetAllAsyncVA();
        var vacationsAbsencesDto = vacations.Select(c => new VacationsAbsencesDto(c)).ToList();
        return vacationsAbsencesDto;
    }

    public async Task<bool> DeleteAsyncVA(int id)
    {
        return await _vacationsAbsences.DeleteAsyncVA(id);
    }

    public async Task<VacationsAbsencesDto> GetByIdVA(int id)
    {
        var vacations = await _vacationsAbsences.GetByIdVA(id);
        if (vacations == null)
            throw new Exception("Vacations or Absences not found");

        var vacationsAbsencesDto = new VacationsAbsencesDto(vacations);
        return vacationsAbsencesDto;
    }
    
    public async Task<bool> ExistByName(string name, int id = 0)
    {
        var vacations = await _vacationsAbsences.GetByName(name, id);
        return vacations != null;
    }
}