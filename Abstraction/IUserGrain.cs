using Abstraction.Models;

namespace Abstraction;

public interface IUserGrain : IGrainWithGuidKey
{
    Task<User> GetUser();
    
    Task AddUser(User user, Guid usersId);
    Task DeleteUser(Guid usersId);
    Task Update(User user);

}