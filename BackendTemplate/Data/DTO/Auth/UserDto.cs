using BackendTemplate.Data.Entities.Auth;

namespace BackendTemplate.Data.DTO.Auth;

public class UserDto
{
    public int Id { get; set; }
    
    public required string Email { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }

    public static explicit operator UserDto(User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
    };
}