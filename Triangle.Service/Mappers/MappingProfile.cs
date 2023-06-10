using AutoMapper;
using Triangle.Domain.Entities;
using Triangle.Service.DTOs.Users;

namespace Triangle.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();

    }
}
