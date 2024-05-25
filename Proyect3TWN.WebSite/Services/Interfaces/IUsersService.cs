using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IUsersService
{
    Task<Response<List<UsersDto>>> GetAllAsync();

    Task<Response<UsersDto>> GetById(int id);

    Task<Response<UsersDto>> SaveAsync(UsersDto usersDto);

    Task<Response<UsersDto>> UpdateAsync(UsersDto usersDto);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<LoginDto>> AuthenticateAsync(string email, string password);

}