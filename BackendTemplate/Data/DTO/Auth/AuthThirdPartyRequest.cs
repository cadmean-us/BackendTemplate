using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Data.DTO.Auth;

public class AuthThirdPartyRequest : DtoBase
{
    [Required]
    public string Token { get; set; }
    
    [Required]
    public string Provider { get; set; }
    
    [Required]
    public string Device { get; set; }
}