using Dapper;
using Dapper.Contrib.Extensions;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.DataAccess.Interfaces;
using Proyect2TWM.Api.Repositories.Interfecies;


namespace Proyect2TWM.Api.Repositories;

public class UsersRepository: IUsersRepository
{
    //Se prepara la clase para saber que se estara trabajando con una base de datos 
    private readonly IDbContext _dbContext;

    public UsersRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Users> SaveAsycnUser(Users users)
    {
        users.id = await _dbContext.Connection.InsertAsync(users);
        return users;
    }

    public async Task<Users> UpdateAsyncUser(Users users)
    {
        await _dbContext.Connection.UpdateAsync(users);
        return users;
    }

    public async Task<List<Users>> GetAllAsyncUser()
    {
        const string sql = "SELECT * FROM Users WHERE isDeleted = 0";
        var users = await _dbContext.Connection.QueryAsync<Users>(sql);
        return users.ToList();
    }

    public async Task<bool> DeleteAsyncUser(int id)
    {
        var users = await GetByIdUser(id);
        if (users == null)
            return false;
        users.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(users);
    }

    public async Task<Users> GetByIdUser(int id)
    {
        var users = await _dbContext.Connection.GetAsync<Users>(id);
        if (users == null)
            return null;
        return users.IsDeleted == true ? null : users;
    }
    
    public async Task<Users> GetByEmailAndPasswordAsync(string email, string password, string username)
    {
        const string sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password AND UserName = @UserName AND isDeleted = 0";
        var user = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(sql, new { Email = email, Password = password, UserName = username });
        return user;
    }

}