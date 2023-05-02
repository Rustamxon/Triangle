using Triangle.Domain.Configurations;
using Triangle.Domain.Entities;
using Triangle.Service.DTOs.Users;

namespace Triangle.Service.Interfaces;

public interface IUserService
{
    Task<UserForResultDto> AddAsync(UserForCreationDto dto); // checked
    Task<UserForResultDto> RetrieveByIdAsync(int id); // checked
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params); // checked
    Task<bool> RemoveAsync(int id); // checked
    Task<bool> DestroyAsync(int id); // checked
    Task<UserForResultDto> ModifyOwnInfoAsync(UserForUpdateDto dto); // checked
    Task<UserForResultDto> ModifyAsAdminAsync(int id, UserForUpdateDto dto); // checked
    Task<UserForResultDto> ChangeEmailAsync(UserForChangeEmailAsync dto); // checked
    Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto); // checked
    Task<UserForResultDto> ChangePhoneNumberAsync(UserForChangePhoneNumberDto dto); // checked
    Task<User> RetrieveByEmailAsync(string email);
}
