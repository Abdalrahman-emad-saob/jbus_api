namespace API.Interfaces
{
    public interface INotisTokenRepository
    {
        bool StoreDeviceToken(int Id, string deviceToken);
        string? GetDeviceToken(int? passengerId);
        bool SaveChanges();
    }
}