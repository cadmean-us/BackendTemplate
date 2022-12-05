namespace BackendTemplate.Data.Entities.Auth;

public class User : EntityBase
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Password { get; set; }
    public required List<UserRole> Roles { get; set; }
    public required UserStatus Status { get; set; }
    public string? RefreshToken { get; set; }
    public string? AppleId { get; set; }
    public string? GoogleId { get; set; }

    public bool HasRole(UserRole role)
    {
        return Roles.Contains(role);
    }
}

public enum UserRole
{
    User,
    Admin,
}

public enum UserStatus
{
    Inactive,
    Active,
    Blocked,
}