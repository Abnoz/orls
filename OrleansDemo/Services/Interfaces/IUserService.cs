using Abstraction.Models;
using OrleansDemo.Models;

namespace OrleansDemo.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUser(Guid id);
    Task<List<User>> GetUsers(Guid id);
    Task AddUser(UserModel user, Guid usersId);
    Task DeleteUser(Guid id, Guid usersId);
}