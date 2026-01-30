using authentication_micro_service.entities;
using Microsoft.EntityFrameworkCore;

namespace authentication_micro_service.repositories;

public class UserRepository(AuthenticationDbContext context) : IUserRepository
{
    public async Task<User> CreateUser(User user)
    {
        var operationAdd = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return operationAdd.Entity;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await context.Users.FirstAsync(u => u.Email != null && u.Email.Equals(email));
    }

    public async Task<User?> GetUserByName(string name)
    {
        return await context.Users.FirstAsync(u => u.Email != null && u.Email.Equals(name));
    }
}