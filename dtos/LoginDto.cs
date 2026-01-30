using System.ComponentModel.DataAnnotations;

namespace authentication_micro_service.dtos;

public record LoginDto(
    string? Username, 
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    string Password, 
    string? Email);