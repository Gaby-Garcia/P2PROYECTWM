using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IVacationsAbsencesService
{
    Task<bool> VacationsAbsencesExist(int id);

    Task<List<VacationsAbsencesDto>> GetAllAsyncVA();


    Task<VacationsAbsencesDto> SaveAsycnVA(VacationsAbsencesDto vacationsAbsences);

    Task<VacationsAbsencesDto> GetByIdVA(int id);

    Task<VacationsAbsencesDto> UpdateAsyncVA(VacationsAbsencesDto vacationsAbsences);

    Task<bool> DeleteAsyncVA(int id);
}