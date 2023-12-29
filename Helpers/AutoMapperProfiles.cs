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

            CreateMap<DriverTrip, DriverTripDto>()
            .ForMember(dtd => dtd.status, opt => opt.MapFrom(dt => dt.status.ToString()));
            CreateMap<DriverTripCreateDto, DriverTrip>()
            .ForMember(dtc => dtc.status, opt => opt.MapFrom(dt => Enum.Parse<TripStatus>(dt.status!)));

            CreateMap<FavoritePoint, FavoritePointDto>();

            CreateMap<Fazaa, FazaaDto>();
            CreateMap<FazaaCreateDto, Fazaa>();
            CreateMap<FazaaUpdateDto, Fazaa>();

            CreateMap<Friends, FriendsDto>();
            CreateMap<FriendsCreateDto, Friends>();

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

            CreateMap<PredefinedStops, PredefinedStopsDto>();
            CreateMap<PredefinedStopsCreateDto, PredefinedStops>();

            CreateMap<Entities.Route, RouteDto>();
            CreateMap<RouteCreateDto, Entities.Route>();
            CreateMap<RouteUpdateDto, Entities.Route>();

            CreateMap<Trip, TripDto>();
            CreateMap<TripCreateDto, Trip>();

            CreateMap<User, UserDto>()
            .ForMember(ud => ud.Sex, opt => opt.MapFrom(u => u.Sex.ToString()))
            .ForMember(ud => ud.Role, opt => opt.MapFrom(u => u.Role.ToString()));

            CreateMap<UserUpdateDto, User>();
            CreateMap<UserDto, User>();
        }
    }
}