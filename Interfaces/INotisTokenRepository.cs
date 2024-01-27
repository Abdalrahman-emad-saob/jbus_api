namespace API.Interfaces
{
    public interface INotisTokenRepository
    {
        Task<bool> StoreDeviceToken(int Id, string deviceToken);
        Task<string?> GetDeviceToken(int? passengerId);
        Task<List<string?>> GetDeviceTokens();
        Task<bool> SaveChanges();
    }
}