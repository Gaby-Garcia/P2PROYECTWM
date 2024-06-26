﻿using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IUsersService
{
    Task<bool> UsersExist(int id);

    Task<List<UsersDto>> GetAllAsyncU();


    Task<UsersDto> SaveAsycnU(UsersDto users);

    Task<UsersDto> GetByIdU(int id);

    Task<UsersDto> UpdateAsyncU(UsersDto users);

    Task<bool> DeleteAsyncU(int id);
    
    Task<bool> AuthenticateAsync(string email, string password);
    
    Task<bool> ExistByUser(string userName, int id = 0);
    Task<bool> ExistByEmail(string email, int id = 0);
}