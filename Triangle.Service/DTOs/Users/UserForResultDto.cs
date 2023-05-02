using System.ComponentModel;
using Triangle.Domain.Enums;

namespace Triangle.Service.DTOs.Users;

public class UserForResultDto
{
    public int Id { get; set; }

    [DisplayName("FirstName")]
    public string FirstName { get; set; }

    [DisplayName("LastName")]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public UserRole Role { get; set; }
}
