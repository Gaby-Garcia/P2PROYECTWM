using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class UsersDto : DtoBase
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UsersDto()
    {
        
    }

    public UsersDto(Users users)
    {
        id = users.id;
        UserName = users.UserName;
        Email = users.Email;
        Password = users.Password;
    }
}