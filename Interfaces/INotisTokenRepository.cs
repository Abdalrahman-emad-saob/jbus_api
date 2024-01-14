namespace API.Interfaces
{
    public interface INotisTokenRepository
    {
        bool StoreDeviceToken(int Id, string deviceToken);
        string? GetDevieToken(int? passengerId);
        bool SaveChanges();
    }
}