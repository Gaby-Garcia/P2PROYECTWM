using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IVacationsAbsencesRepository
{
    Task<VacationsAbsences> SaveAsycnVA(VacationsAbsences vacationsAbsences);
    
    Task<VacationsAbsences> UpdateAsyncVA(VacationsAbsences vacationsAbsences);
    
    Task<List<VacationsAbsences>> GetAllAsyncVA();
    
    Task<bool> DeleteAsyncVA(int id);
    
    Task<VacationsAbsences> GetByIdVA(int id);
    
    Task<VacationsAbsences> GetByName(string name, int id = 0);
}