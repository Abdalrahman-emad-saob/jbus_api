using API.Interfaces;

namespace API.Data
{
    public class NotisTokenRepository : INotisTokenRepository
    {
        private readonly DataContext _context;

        public NotisTokenRepository(
            DataContext context
        )
        {
            _context = context;
        }

        public string? GetDevieToken(int? passengerId)
        {
            var passenger = _context.Passengers.Find(passengerId);
            if(passenger == null)
                return null;
                
            return passenger.FcmToken;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public bool StoreDeviceToken(int Id, string deviceToken)
        {
            var passenger = _context.Passengers.Find(Id);

            if (passenger == null)
                return false;

            passenger.FcmToken = deviceToken;
            SaveChanges();
            return true;
        }
    }
}