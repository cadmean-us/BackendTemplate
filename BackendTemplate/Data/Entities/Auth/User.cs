namespace BackendTemplate.Data.Entities.Auth;

public class User : EntityBase
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Password { get; set; }
    public List<UserRole> Roles { get; set; }
    public UserStatus Status { get; set; }
    public string? RefreshToken { get; set; }

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