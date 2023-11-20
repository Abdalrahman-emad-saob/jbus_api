using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Passenger, PassengerDto>();
            // .ForMember(dest => dest.User.Age, opt => opt.MapFrom(src => src.User.GetAge()));
            CreateMap<User, UserDto>();
        }
    }
}