using System.ComponentModel.DataAnnotations;

namespace Triangle.Service.DTOs.Users;

public class UserForChangeEmailAsync
{
    [Required(ErrorMessage = "It is required")]
    [EmailAddress(ErrorMessage = "Old email address must not be null or empty!")]
    public string OldEmail { get; set; }


    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }


    [Required(ErrorMessage = "It is required")]
    [EmailAddress(ErrorMessage = "New email address must not be null or empty!")]
    public string NewEmail { get; set; }
}
