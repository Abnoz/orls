using System.Security.Permissions;
using Abstraction;
using Abstraction.Models;
using Orleans.Runtime;

namespace Grains;

public class UserGrain : Grain,IUserGrain
{
    private IPersistentState<User> _user;
    private readonly IGrainFactory _grainFactory;
    public UserGrain(
        [PersistentState(stateName: "User", storageName: "demoGrainStorage")]
        IPersistentState<User> user,
        IGrainFactory grainFactory)
    {
        _user = user;
        _grainFactory = grainFactory;
    }


    public async Task DeleteUser(Guid usersId)
    {
        var usersGrain = _grainFactory.GetGrain<IUsersGrain>(usersId);
        await usersGrain.DeleteUser(_user.State);
        await _user.ClearStateAsync();

    }

    public async Task AddUser(User user, Guid usersId)
    {
        _user.State = user;
        await _user.WriteStateAsync();
        var usersGrain = _grainFactory.GetGrain<IUsersGrain>(usersId);
        await usersGrain.AddUser(user);

    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUser()
    {
        return _user.State;
    }
}