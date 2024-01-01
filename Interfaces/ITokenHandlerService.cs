namespace API.Interfaces
{
    public interface ITokenHandlerService
    {
        int TokenHandler();
        string ExtractUserRole();
    }
}