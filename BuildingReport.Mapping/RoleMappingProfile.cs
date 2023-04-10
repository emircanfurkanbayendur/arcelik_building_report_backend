using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<UpdateRoleRequest, Role>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Role, UpdateRoleRequest>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


        CreateMap<Role, RoleRequest>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<Role, RoleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<RoleRequest, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<RoleResponse, Role>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


    }
}