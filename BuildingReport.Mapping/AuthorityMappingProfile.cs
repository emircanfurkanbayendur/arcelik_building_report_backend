using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.Entities;

public class AuthorityMappingProfile : Profile
{
    public AuthorityMappingProfile()
    {
        CreateMap<AuthorityDTO, Authority>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Authority, AuthorityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}