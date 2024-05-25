using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Repositories.Interfecies;

public interface IUsersRepository
{
    Task<Users> SaveAsycnUser(Users users);
    
    Task<Users> UpdateAsyncUser(Users users);
    
    Task<List<Users>> GetAllAsyncUser();
    
    Task<bool> DeleteAsyncUser(int id);
    
    Task<Users> GetByIdUser(int id);
    
    Task<Users> GetByEmailAndPasswordAsync(string email, string password);
    
    Task<Users> GetByName(string userName, int id = 0);
    
    Task<Users> GetByEmail(string email, int id = 0);

    
}