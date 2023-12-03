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
            CreateMap<PassengerUpdateDto, Passenger>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RouteUpdateDto, Entities.Route>();
            CreateMap<Entities.Route, RouteDto>();
            CreateMap<InterestPoint, InterestPointDto>();
            CreateMap<Point, PointDto>();
            CreateMap<ChargingTransaction, ChargingTransactionDto>();
        }
    }
}