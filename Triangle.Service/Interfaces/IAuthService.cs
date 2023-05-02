using Triangle.Service.DTOs.Login;

namespace Triangle.Service.Interfaces;

public interface IAuthService
{
    Task<LoginForResultDto> AuthenticateAsync(string email, string password);
}
