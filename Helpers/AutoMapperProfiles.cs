using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminCreateDto, Admin>();
            CreateMap<AdminUpdateDto, Admin>()
            .ForMember(p => p.UserId, opt => opt.Ignore())
            .ForMember(p => p.User, opt => opt.Ignore());

            CreateMap<Bus, BusDto>();
            CreateMap<BusCreateDto, Bus>();
            CreateMap<BusUpdateDto, Bus>();

            CreateMap<ChargingTransaction, ChargingTransactionDto>()
            .ForMember(ctd => ctd.chargingMethod, opt => opt.MapFrom(ct => ct.ChargingMethod.ToString()));
            CreateMap<ChargingTransactionCreateDto, ChargingTransaction>();

            CreateMap<Driver, DriverDto>();
            CreateMap<DriverCreateDto, Driver>();
            CreateMap<DriverUpdateDto, Driver>()
            .ForMember(p => p.UserId, opt => opt.Ignore())
            .ForMember(p => p.User, opt => opt.Ignore());

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
            CreateMap<PassengerUpdateDto, Passenger>()
            .ForMember(p => p.UserId, opt => opt.Ignore())
            .ForMember(p => p.User, opt => opt.Ignore())
            .ForMember(p => p.CreditorId, opt => opt.Ignore())
            .ForMember(p => p.Creditor, opt => opt.Ignore())
            .ForMember(p => p.InDebtId, opt => opt.Ignore())
            .ForMember(p => p.InDebt, opt => opt.Ignore());

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

            CreateMap<Trip, TripDto>()
            .ForMember(td => td.Status, opt => opt.MapFrom(t => t.status.ToString()));
            CreateMap<TripUpdateDto, Trip>()
            .ForMember(tud => tud.status, opt => opt.MapFrom(t => t.Status!.ToString()));
            CreateMap<TripCreateDto, Trip>()
            .ForMember(t => t.status, opt => opt.MapFrom(tcd => tcd.status!.ToString()));

            CreateMap<User, UserDto>()
            .ForMember(ud => ud.Sex, opt => opt.MapFrom(u => u.Sex.ToString()))
            .ForMember(ud => ud.Role, opt => opt.MapFrom(u => u.Role.ToString()));
            CreateMap<UserUpdateDto, User>()
            .ForMember(u => u.Sex, opt => opt.MapFrom(uud => Enum.Parse<Sex>(uud.Sex!)));
            CreateMap<UserDto, User>();
        }
    }
}