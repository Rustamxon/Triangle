﻿using Triangle.Domain.Enums;

namespace Triangle.Service.DTOs.Users;

public class UserForResultDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}
