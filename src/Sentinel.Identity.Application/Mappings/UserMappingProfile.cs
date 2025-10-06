using AutoMapper;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Entities;

namespace Sentinel.Identity.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserListDto>();
        CreateMap<UserWriteDto, User>();
    }
}