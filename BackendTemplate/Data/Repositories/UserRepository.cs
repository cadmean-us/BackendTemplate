using BackendTemplate.Data.Entities.Auth;
using BackendTemplate.Database;
using BackendTemplate.Exceptions;
using BackendTemplate.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackendTemplate.Data.Repositories;

public class UserRepository : RepositoryBase<User>
{
    public UserRepository(CadDbContext context) : base(context)
    {
    }

    public User FindByEmail(string email)
    {
        return Find(u => u.Email == email).FirstOrDefault() ?? throw new EntityNotFoundException();
    }
    
    public bool CheckUserExistsByEmail(string email)
    {
        return Find(u => u.Email == email).Any();
    }
    
    public Task<User> FindByAuthAsync(string auth)
    {
        var claims = JwtHelper.GetTokenClaims(auth);
        return FindByIdAsync(claims.UserId);
    }

    // public Task<User> FindByAuthGreedyAsync(string auth)
    // {
    //     var claims = JwtHelper.GetTokenClaims(auth);
    //     return DbSet.Include(u => u.PrimaryLanguage)
    //         .SingleAsync(u => u.Id == claims.UserId);
    // }
}