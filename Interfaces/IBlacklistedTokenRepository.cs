namespace API.Interfaces
{
    public interface IBlacklistedTokenRepository
    {
        public Task BlacklistTokenAsync(string token);
    }
}