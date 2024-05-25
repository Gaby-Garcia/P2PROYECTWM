using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public LoginDto()
    {
        
    }

    public LoginDto(Users users)
    {
        Email = users.Email;
        Password = users.Password;
    }
}