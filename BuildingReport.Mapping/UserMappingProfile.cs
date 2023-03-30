using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.Entities;
using BuildingReport.Business;
using System.Security.Policy;
using BuildingReport.Business.Abstract;

public class UserMappingProfile : Profile
{
    private readonly IHashService _hash;

    public UserMappingProfile()
    {
    }
    public UserMappingProfile(IHashService hash)
    {
        _hash = hash;


        CreateMap<User, ReturnDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<ReturnDto, User>();


        CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => _hash.HashPassword(src.Password)));
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

    }

    public IHashService Hash => _hash;
}
