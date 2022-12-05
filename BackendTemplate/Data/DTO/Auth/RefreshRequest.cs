using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Data.DTO.Auth;

public class RefreshRequest : DtoBase
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public string RefreshToken { get; set; } = "";
}