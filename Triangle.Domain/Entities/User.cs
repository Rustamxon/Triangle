using Triangle.Domain.Commons;
using Triangle.Domain.Enums;

namespace Triangle.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.Unblocked;
}
