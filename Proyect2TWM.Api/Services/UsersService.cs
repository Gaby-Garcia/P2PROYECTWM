using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<bool> UsersExist(int id)
    {
        var user = await _usersRepository.GetByIdUser(id);
        return (user != null);
    }

    public async Task<UsersDto> SaveAsycnU(UsersDto usersDto)
    {
        var users = new Users
        {
            UserName = usersDto.UserName,
            Email = usersDto.Email,
            Password = usersDto.Password,
            CreatedBy = "Gaby-Garcia",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Gaby-Garcia",
            UpdatedDate = DateTime.Now
        };
        users = await _usersRepository.SaveAsycnUser(users);
        usersDto.id = users.id;

        return usersDto;
    }

    public async Task<UsersDto> UpdateAsyncU(UsersDto usersDto)
    {
        var users = await _usersRepository.GetByIdUser(usersDto.id);
        if (users == null)
            throw new Exception("User Not Found");
        users.UserName = usersDto.UserName;
        users.Email = usersDto.Email;
        users.Password = usersDto.Password;
        users.UpdatedBy = "Gaby-Garcia";
        users.UpdatedDate = DateTime.Now;
        
        await _usersRepository.UpdateAsyncUser(users);
        return usersDto;
    }

    public async Task<List<UsersDto>> GetAllAsyncU()
    {
        var users = await _usersRepository.GetAllAsyncUser();
        var usersDto = users.Select(c => new UsersDto(c)).ToList();
        return usersDto;
    }

    public async Task<bool> DeleteAsyncU(int id)
    {
        return await _usersRepository.DeleteAsyncUser(id);
    }
    

    public async Task<UsersDto> GetByIdU(int id)
    {
        var users = await _usersRepository.GetByIdUser(id);
        if (users == null)
            throw new Exception("User not found");

        var userDto = new UsersDto(users);
        return userDto;
    }
    
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        var user = await _usersRepository.GetByEmailAndPasswordAsync(email, password);
        return user != null;

    }
    
    public async Task<bool> ExistByUser(string userName, int id = 0)
    {
        var user = await _usersRepository.GetByName(userName, id);
        return user != null;
    }
    
    
    public async Task<bool> ExistByEmail(string email, int id = 0)
    {
        var user = await _usersRepository.GetByEmail(email, id);
        return user != null;
    }

} 