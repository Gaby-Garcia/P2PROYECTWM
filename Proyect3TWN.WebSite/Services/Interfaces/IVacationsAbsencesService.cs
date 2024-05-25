using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IVacationsAbsencesService
{
    Task<Response<List<VacationsAbsencesDto>>> GetAllAsync();

    Task<Response<VacationsAbsencesDto>> GetById(int id);

    Task<Response<VacationsAbsencesDto>> SaveAsync(VacationsAbsencesDto vacationsAbsencesDto);

    Task<Response<VacationsAbsencesDto>> UpdateAsync(VacationsAbsencesDto vacationsAbsencesDto);

    Task<Response<bool>> DeleteAsync(int id);
}