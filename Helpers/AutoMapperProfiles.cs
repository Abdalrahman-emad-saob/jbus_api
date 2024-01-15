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
            CreateMap<Passenger, FriendDto>();

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
            .ForMember(p => p.InDebt, opt => opt.Ignore())
            .ForMember(p => p.ProfileImage, opt => opt.PreCondition(src => src.ProfileImage != null)); ;

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
            .ForMember(tud => tud.status, opt => opt.MapFrom(t => t.Status!.ToString()))
            .ForMember(t => t.FinishedAt, opt => opt.PreCondition(src => src.FinishedAt != default))
            .ForMember(t => t.status, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Status)))
            .ForMember(t => t.PaymentTransactionId, opt => opt.PreCondition(src => src.PaymentTransactionId != default))
            .ForMember(t => t.PickUpPoint, opt => opt.PreCondition(src => src.PickUpPoint != null))
            .ForMember(t => t.DropOffPoint, opt => opt.PreCondition(src => src.DropOffPoint != null));;
            CreateMap<TripCreateDto, Trip>()
            .ForMember(t => t.status, opt => opt.MapFrom(tcd => Enum.Parse<TripStatus>(tcd.status!)));
               


            CreateMap<User, UserDto>()
            .ForMember(ud => ud.Sex, opt => opt.MapFrom(u => u.Sex.ToString()))
            .ForMember(ud => ud.Role, opt => opt.MapFrom(u => u.Role.ToString()));
            CreateMap<UserUpdateDto, User>()
            .ForMember(u => u.Sex, opt => opt.MapFrom(uud => Enum.Parse<Sex>(uud.Sex!)))
            .ForMember(u => u.Name, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Name)))
            .ForMember(u => u.Email, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Email)))
            .ForMember(u => u.Sex, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Sex)));
            CreateMap<UserDto, User>();
        }
    }
}
