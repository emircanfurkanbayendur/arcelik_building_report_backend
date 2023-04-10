using AutoMapper;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;


public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {


        CreateMap<User, LoginResponse>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<LoginResponse, User>();


        CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<User, UpdateUserRequest>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());


        CreateMap<UserRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<User, UserResponse>()
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<UserResponse, User>()
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}
