using Triangle.Domain.Entities;
using Triangle.Service.DTOs.Users;

namespace Triangle.Service.Interfaces;

public interface IUserService
{
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync();
    Task<User> RetrieveByEmailAsync(string email);
    Task<bool> RemoveAsync(int id);
    Task<bool> BlockAsync(int id);
    Task<bool> UnBlockAsync(int id);
}
