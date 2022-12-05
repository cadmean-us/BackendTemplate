using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Data.DTO.Auth;

public class AuthPasswordRequest : DtoBase
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    
    [Required]
    public string Password { get; set; } = "";
}