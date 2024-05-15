using Abstraction.Models;

namespace Abstraction;

public interface IUsersGrain : IGrainWithGuidKey
{
    Task<List<User>> GetUsers();
    Task AddUser(User user);
    Task DeleteUser(User user);


}