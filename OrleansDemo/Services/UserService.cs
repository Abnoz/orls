using Abstraction;
using Abstraction.Models;
using AutoMapper;
using OrleansDemo.Models;
using OrleansDemo.Services.Interfaces;

namespace OrleansDemo.Services;

public class UserService : IUserService
{
    private readonly IClusterClient _client;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, IClusterClient client)
    {
        _client = client;
        _mapper = mapper;
    }


    public async Task<User> GetUser(Guid id)
    {
        var userGrain = _client.GetGrain<IUserGrain>(id);
        return await userGrain.GetUser();
    }

    public Task<List<User>> GetUsers(Guid id)
    {
        var usersGrain = _client.GetGrain<IUsersGrain>(id);
        return usersGrain.GetUsers();
    }


    public async Task AddUser(UserModel user, Guid userId)
    {
        var userGrain = _client.GetGrain<IUserGrain>(user.Id);
        var userToBeInserted = _mapper.Map<User>(user);
        
        await userGrain.AddUser(userToBeInserted, userId);
    }

    public async Task DeleteUser(Guid id, Guid usersId)
    {
        var userGrain = _client.GetGrain<IUserGrain>(id);
        await userGrain.DeleteUser(usersId);
    }
}