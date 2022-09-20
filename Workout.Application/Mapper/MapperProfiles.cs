using AutoMapper;
using Workout.Application.Models;
using Workout.Core.Entities;

namespace Workout.Application.Mappers;

public sealed class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<User, UserLoginModel>().ReverseMap();
    }
}
