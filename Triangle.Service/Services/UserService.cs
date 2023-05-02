using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Triangle.Data.IRepositories;
using Triangle.Domain.Configurations;
using Triangle.Domain.Entities;
using Triangle.Service.DTOs.Users;
using Triangle.Service.Exceptions;
using Triangle.Service.Extensions;
using Triangle.Service.Interfaces;
using Triangle.Shared.Helpers;

namespace Triangle.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> repository;

    public UserService()
    {
    }

    public UserService(IMapper mapper, IRepository<User> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var existUser = await this.repository.SelectAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));
        if (existUser is not null && !existUser.IsDeleted)
            throw new CustomException(404, "User is already exist");

        // Checking user's phone number is valid?
        //var context = new ValidationContext(dto);
        //var results = new List<ValidationResult>();
        //if (!Validator.TryValidateObject(dto, context, results, true))
        //    throw new ArgumentException(string.Join("; ", results.Select(r => r.ErrorMessage)));

        var mapped = this.mapper.Map<User>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password = PasswordHelper.Hash(dto.Password);
        User addedModel = await this.repository.InsertAsync(mapped);

        await this.repository.SaveChangesAsync();

        return this.mapper.Map<UserForResultDto>(addedModel);
    }

    public async Task<UserForResultDto> ChangeEmailAsync(UserForChangeEmailAsync dto)
    {
        var existUser = await this.repository.SelectAsync(u => u.Email.ToLower().Equals(dto.NewEmail.ToLower()));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");

        if (!existUser.Email.ToLower().Equals(dto.OldEmail.ToLower()))
            throw new CustomException(400, "The email you wrote for the old email does not match your email");

        if (!PasswordHelper.Verify(dto.Password, existUser.Password))
            throw new CustomException(400, "Password is incorrect");

        if (existUser.Email.ToLower().Equals(dto.NewEmail.ToLower()))
            throw new CustomException(400, "Please, enter another email, It is the same with your old email");

        existUser.Email = dto.NewEmail;
        await this.repository.SaveChangesAsync();
        return this.mapper.Map<UserForResultDto>(existUser);
    }

    public async Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto)
    {
        var existUser = await this.repository.SelectAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");
        if (!PasswordHelper.Verify(dto.OldPassword, existUser.Password))
            throw new CustomException(400, "Password is incorrect");
        if (dto.NewPassword != dto.ConfirmNewPassword)
            throw new CustomException(400, "New password and confirm password are not equal");

        existUser.Password = PasswordHelper.Hash(dto.NewPassword);

        await this.repository.SaveChangesAsync();

        return this.mapper.Map<UserForResultDto>(existUser);
    }

    public async Task<UserForResultDto> ChangePhoneNumberAsync(UserForChangePhoneNumberDto dto)
    {
        var existUser = await this.repository.SelectAsync(u => u.Id.Equals(HttpContextHelper.UserId));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");
        if (existUser.Phone.Equals(dto.Phone))
            throw new CustomException(400, "Please, enter another phone number. It is the same with your old phone number");
        existUser.Phone = dto.Phone;

        await this.repository.SaveChangesAsync();

        return this.mapper.Map<UserForResultDto>(existUser);
    }

    public async Task<bool> DestroyAsync(int id)
    {
        var existUser = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");

        await this.repository.DeleteAsync(existUser);
        await this.repository.SaveChangesAsync();
        return true;
    }

    public async Task<UserForResultDto> ModifyAsAdminAsync(int id, UserForUpdateDto dto)
    {
        var existUser = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");
        this.mapper.Map(dto, existUser);
        existUser.LastUpdatedAt = DateTime.UtcNow;
        existUser.UpdatedBy = HttpContextHelper.UserId;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<UserForResultDto>(existUser);
    }

    public async Task<UserForResultDto> ModifyOwnInfoAsync(UserForUpdateDto dto)
    {
        var userId = HttpContextHelper.UserId;
        var existUser = await this.repository.SelectAsync(u => u.Id.Equals(userId));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");
        this.mapper.Map(dto, existUser);
        existUser.LastUpdatedAt = DateTime.UtcNow;
        existUser.UpdatedBy = userId;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<UserForResultDto>(existUser);
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var existUser = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");
        existUser.IsDeleted = true;
        existUser.Deletedby = HttpContextHelper.UserId;
        await this.repository.SaveChangesAsync();

        return true;

    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await this.repository.SelectAll()
            .Where(u => u.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<User> RetrieveByEmailAsync(string email)
        => await this.repository.SelectAsync(u => u.Email.ToLower().Equals(email.ToLower()));

    public async Task<UserForResultDto> RetrieveByIdAsync(int id)
    {
        var existUser = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existUser is null || existUser.IsDeleted)
            throw new CustomException(404, "User not found");

        return this.mapper.Map<UserForResultDto>(existUser);
    }
}
