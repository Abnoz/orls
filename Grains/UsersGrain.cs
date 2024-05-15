using Abstraction;
using Abstraction.Models;
using Orleans.Runtime;

namespace Grains;

public class UsersGrain : IUsersGrain
{
    
    private IPersistentState<List<User>> _users;

    public UsersGrain(
        [PersistentState(stateName:"Users", storageName: "demoGrainStorage")]
        IPersistentState<List<User>> users
    )
    {
        _users = users;
    }
    public async Task<List<User>> GetUsers()
    {
        return _users.State;
    }

    public async Task AddUser(User user)
    {
        _users.State.Add(user);
        await _users.WriteStateAsync();
    }

    public async Task DeleteUser(User user)
    {
        _users.State.Remove(user);
        await _users.WriteStateAsync();
    }
}