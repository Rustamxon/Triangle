using System.ComponentModel.DataAnnotations;

namespace Triangle.Service.DTOs.Users;

public class UserForChangePhoneNumberDto
{
    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    public string Phone { get; set; }
}
