using aspnetUserAuthTemplate.Models.Database;
using aspnetUserAuthTemplate.Models.Request;
using aspnetUserAuthTemplate.Modules;
using aspnetUserAuthTemplate.Shared;
using Microsoft.EntityFrameworkCore;

namespace aspnetUserAuthTemplate.Repository;

public class UserRepository
{

    private readonly DatabaseContext ctx;
    private readonly IPasswordHasher hasher;

    public UserRepository(DatabaseContext ctx, IPasswordHasher hasher)
    {
        this.ctx = ctx;
        this.hasher = hasher;
    }

    public async Task<User> RegisterUser(RegisterRequest request)
    {
        var user = new User();
        user.Username = request.Username;
        user.Password = hasher.HashFromPassword(request.Password);
        ctx.Add(user);
        await ctx.SaveChangesAsync();
        return user;
    }

    public async Task<bool> LoginUser(LoginRequest request)
    {
        var user = await FindUserByUsername(request.Username);
        if (user == null)
        {
            return false;
        }

        if (!hasher.CompareHashAndPassword(user.Password, request.Password)) return false;
        return true;
    }

    public async Task<User?> FindUserByUsername(string username)
    {
        return await ctx.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }

}