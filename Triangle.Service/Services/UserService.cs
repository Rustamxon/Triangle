using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Triangle.Data.IRepositories;
using Triangle.Domain.Entities;
using Triangle.Service.DTOs.Users;
using Triangle.Service.Exceptions;
using Triangle.Service.Interfaces;
using Triangle.Shared.Helpers;

namespace Triangle.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> _repository;

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var user = await this._repository.SelectAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));
        if (user != null && !user.IsDeleted)
            throw new CustomException(409, "User already exist");
        if (String.IsNullOrEmpty(dto.Password))
            throw new CustomException(400, "You cannot enter password like this");

        var mapped = this.mapper.Map<User>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password = PasswordHelper.Hash(dto.Password);
        var addedModel = await this._repository.InsertAsync(mapped);
        await this._repository.SaveChangesAsync();

        return this.mapper.Map<UserForResultDto>(addedModel);
    }

    public async Task<bool> BlockAsync(int id)
    {
        var user = await this._repository.SelectAsync(u => u.Id == id);
        if (user is null || user.IsDeleted)
        {
            throw new CustomException(404, "User not found");
        }
        user.Role = Domain.Enums.UserRole.Blocked;
        user.LastUpdatedAt = DateTime.UtcNow;
        user.BlockedBy = HttpContextHelper.UserId;
        await this._repository.SaveChangesAsync();
        return true;

    }
    public async Task<bool> UnBlockAsync(int id)
    {
        var user = await this._repository.SelectAsync(u => u.Id == id);
        if (user is null || user.IsDeleted)
        {
            throw new CustomException(404, "User not found");
        }
        user.Role = Domain.Enums.UserRole.Unblocked;
        user.LastUpdatedAt = DateTime.UtcNow;
        user.BlockedBy = HttpContextHelper.UserId;
        await this._repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var user = await this._repository.SelectAsync(u => u.Id == id);
        if (user is null || user.IsDeleted)
        {
            throw new CustomException(404, "User not found");
        }
        user.Deletedby = HttpContextHelper.UserId;

        await this._repository.DeleteAsync(u => u.Id == id);
        await this._repository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync()
    {
        var users = await this._repository.SelectAll()
            .Where(u => u.IsDeleted == false)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<User> RetrieveByEmailAsync(string email)
    {
        return await this._repository.SelectAsync(u => u.Email == email);
    }

}
