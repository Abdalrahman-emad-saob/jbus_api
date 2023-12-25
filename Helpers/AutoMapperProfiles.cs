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
            CreateMap<BusCreateDto, Bus>();
            CreateMap<BusUpdateDto, Bus>();

            CreateMap<ChargingTransaction, ChargingTransactionDto>();
            CreateMap<ChargingTransactionCreateDto, ChargingTransaction>();

            CreateMap<Driver, DriverDto>();
            CreateMap<DriverCreateDto, Driver>();
            CreateMap<DriverUpdateDto, Driver>();

            CreateMap<FavoritePoint, FavoritePointDto>();

            CreateMap<InterestPoint, InterestPointDto>();
            CreateMap<InterestPointCreateDto, InterestPoint>();
            CreateMap<InterestPointUpdateDto, InterestPoint>();
            
            CreateMap<OTP, OTPDto>();
            
            CreateMap<Passenger, PassengerDto>();
            CreateMap<PassengerUpdateDto, Passenger>();

            CreateMap<PaymentTransaction, PaymentTransactionDto>();
            CreateMap<PaymentTransactionCreateDto, PaymentTransaction>();
            
            CreateMap<Point, PointDto>();
            CreateMap<PointCreateDto, Point>();
            CreateMap<PointUpdateDto, Point>();

            CreateMap<Entities.Route, RouteDto>();
            CreateMap<RouteCreateDto, Entities.Route>();
            CreateMap<RouteUpdateDto, Entities.Route>();

            CreateMap<Trip, TripDto>();
            CreateMap<TripCreateDto, Trip>();

            CreateMap<User, UserDto>()
            .ForMember(ud => ud.UserSex, opt => opt.MapFrom(u => u.UserSex.ToString()))
            .ForMember(ud => ud.UserRole, opt => opt.MapFrom(u => u.UserRole.ToString()));

            CreateMap<UserUpdateDto, User>();
            CreateMap<UserDto, User>();
        }
    }
}