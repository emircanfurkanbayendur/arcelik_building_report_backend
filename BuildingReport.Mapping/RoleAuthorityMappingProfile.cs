using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.Entities;

public class RoleAuthorityMappingProfile : Profile
{
    public RoleAuthorityMappingProfile()
    {
        CreateMap<RoleAuthorityDTO, RoleAuthority>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));

        CreateMap<RoleAuthority, RoleAuthorityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));
    }
}
