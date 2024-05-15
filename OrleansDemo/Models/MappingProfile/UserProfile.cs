using Abstraction.Models;
using AutoMapper;

namespace OrleansDemo.Models.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}