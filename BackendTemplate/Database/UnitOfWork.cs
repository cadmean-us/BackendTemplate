using BackendTemplate.Data.Repositories;

namespace BackendTemplate.Database;

public class UnitOfWork
{
    private readonly CadDbContext context = new();
    
    private UserRepository? userRepository;

    public UserRepository UserRepository
    {
        get
        {
            userRepository ??= new UserRepository(context);
            return userRepository;
        }
    }

    public void Save()
    {
        context.SaveChanges();
    }
    
    public Task<int> SaveAsync()
    {
        return context.SaveChangesAsync();
    }
}