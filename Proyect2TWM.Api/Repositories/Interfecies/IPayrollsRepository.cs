using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IPayrollsRepository
{
    Task<Pyrolls> SaveAsycnP(Pyrolls pyrolls);
    
    Task<Pyrolls> UpdateAsyncP(Pyrolls pyrolls);
    
    Task<List<Pyrolls>> GetAllAsyncP();
    
    Task<bool> DeleteAsyncP(int id);
    
    Task<Pyrolls> GetByIdP(int id);
    
    Task<Pyrolls> GetByDatePayment(string paymentDate, int id = 0);
}