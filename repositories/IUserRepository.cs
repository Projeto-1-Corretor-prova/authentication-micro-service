using authentication_micro_service.entities;

namespace authentication_micro_service.repositories;

public interface IUserRepository
{
    Task<User> CreateUser(User user);

    Task<User?> GetUserByEmail(string email);

    Task<User?> GetUserByName(string name);
}