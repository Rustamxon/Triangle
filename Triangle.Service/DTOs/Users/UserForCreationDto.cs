using System.ComponentModel.DataAnnotations;

namespace Triangle.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }


    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }


    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter valid email address")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }


    [Required(ErrorMessage = "Phone number is required")]
    public string Phone { get; set; }
}
