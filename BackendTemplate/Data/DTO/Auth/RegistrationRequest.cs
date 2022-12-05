using System.ComponentModel.DataAnnotations;
using BackendTemplate.Data.Entities.Auth;

namespace BackendTemplate.Data.DTO.Auth;

public class RegistrationRequest : DtoBase
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    
    [Required]
    [MinLength(2)]
    [MaxLength(255)]
    public string FirstName { get; set; } = "";
    
    [Required]
    [MinLength(2)]
    [MaxLength(255)]
    public string LastName { get; set; } = "";
    
    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string Password { get; set; } = "";
    
    [Required]
    [MinLength(2)]
    [MaxLength(2)]
    public string PrimaryLanguageCode { get; set; } = "";

    public static explicit operator User(RegistrationRequest dto) => new()
    {
        Email = dto.Email,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Password = dto.Password,
        Roles = new (),
        Status = UserStatus.Inactive,
    };
}