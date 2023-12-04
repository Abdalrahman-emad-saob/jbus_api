using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Bus, BusDto>();
            CreateMap<ChargingTransaction, ChargingTransactionDto>();
            CreateMap<Driver, DriverDto>();
            CreateMap<FavoritePoint, FavoritePointDto>();
            CreateMap<InterestPoint, InterestPointDto>();
            // TODO
            CreateMap<OTP, OTPDto>();
            
            CreateMap<Passenger, PassengerDto>();
            CreateMap<PassengerUpdateDto, Passenger>();
            CreateMap<PaymentTransaction, PaymentTransactionDto>();
            CreateMap<Point, PointDto>();
            CreateMap<Entities.Route, RouteDto>();
            CreateMap<RouteUpdateDto, Entities.Route>();
            CreateMap<Trip, TripDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}