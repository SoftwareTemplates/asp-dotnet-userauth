using aspnetUserAuthTemplate.Modules;
using aspnetUserAuthTemplate.Repository;

namespace aspnetUserAuthTemplate.Shared;

public class DbAccess
{
    public readonly UserRepository UserRepository;

    public DbAccess(DatabaseContext ctx, IPasswordHasher hasher)
    {
        UserRepository = new UserRepository(ctx, hasher);
    }

}