using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triangle.Domain.Configurations;
using Triangle.Service.DTOs.Users;
using Triangle.Service.Interfaces;

namespace Triangle.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await userService.RetrieveAllAsync(@params));

    [HttpGet("Id")]
    public async ValueTask<IActionResult> GetByIdAsync(int id)
            => Ok(await userService.RetrieveByIdAsync(id));

    [HttpPost, AllowAnonymous]
    public async ValueTask<ActionResult<UserForResultDto>> PostAsync(UserForCreationDto dto)
        => Ok(await userService.AddAsync(dto));

    [HttpPut("Id")]
    public async ValueTask<ActionResult<UserForResultDto>> PutAsync(UserForUpdateDto dto)
        => Ok(await userService.ModifyOwnInfoAsync(dto));

    [HttpPut("id"), Authorize(Roles = "Admin, SuperAdmin")]
    public async ValueTask<ActionResult<UserForResultDto>> PutAsAdminAsync(int id, UserForUpdateDto dto)
        => Ok(await userService.ModifyAsAdminAsync(id, dto));

    [HttpDelete("Id")]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await userService.RemoveAsync(id));

    [HttpDelete("id"), Authorize(Roles = "SuperAdmin")]
    public async ValueTask<ActionResult<bool>> DestroyAsync(int id)
        => Ok(await userService.DestroyAsync(id));

    [HttpPut("change-password")]
    public async ValueTask<ActionResult<UserForResultDto>> ChangePasswordAsync(UserForChangePasswordDto dto)
        => Ok(await userService.ChangePasswordAsync(dto));

    [HttpPut("change-email")]
    public async ValueTask<ActionResult<UserForResultDto>> ChangeEmailAsync(UserForChangeEmailAsync dto)
        => Ok(await userService.ChangeEmailAsync(dto));

    [HttpPut("change-phone-number")]
    public async ValueTask<ActionResult<UserForResultDto>> ChangePhoneNumberAsync(UserForChangePhoneNumberDto dto)
        => Ok(await userService.ChangePhoneNumberAsync(dto));

}
