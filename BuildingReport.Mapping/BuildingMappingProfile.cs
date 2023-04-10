using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.Entities;

public class BuildingMappingProfile : Profile
{
    public BuildingMappingProfile()
    {
        CreateMap<BuildingDTO, Building>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
            .ForMember(dest => dest.Neighbourhood, opt => opt.MapFrom(src => src.Neighbourhood))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.RegisteredAt, opt => opt.MapFrom(src => src.RegisteredAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId));

        CreateMap<Building, BuildingDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
            .ForMember(dest => dest.Neighbourhood, opt => opt.MapFrom(src => src.Neighbourhood))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.RegisteredAt, opt => opt.MapFrom(src => src.RegisteredAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId));



        CreateMap<List<int>, BuildingCountDTO>()
            .ForMember(dest => dest.CityCount, opt => opt.MapFrom(src => src[0]))
            .ForMember(dest => dest.DistrictCount, opt => opt.MapFrom(src => src[1]))
            .ForMember(dest => dest.NeighbourhoodCount, opt => opt.MapFrom(src => src[2]))
            .ForMember(dest => dest.BuildingCount, opt => opt.MapFrom(src => src[3]));

        CreateMap<List<string>, BuildingStreetsDTO>()
            .ForMember(dest => dest.Streets, opt => opt.MapFrom(src => src));

        CreateMap<List<Building>, BuildingListDTO>()
            .ForMember(dest => dest.Buildings, opt => opt.MapFrom(src => src));


        CreateMap<Building, BuildingNameBuildingNumberDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber));

    }
}
