using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.Entities;


public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {


        CreateMap<User, ReturnDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<ReturnDto, User>();


        CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

    }
}
