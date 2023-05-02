using AutoMapper;
using Triangle.Domain.Entities;
using Triangle.Service.DTOs.Users;

namespace Triangle.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<UserForCreationDto, UserForUpdateDto>().ReverseMap();

    }
}
