using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace authentication_micro_service.dtos;

public record RegisterDto(
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
    string Username,
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    string Password,

    [EmailAddress(ErrorMessage =  "Email is invalid")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
    string Email
    );