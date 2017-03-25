namespace Visions.Auth.Contracts
{
    public interface IUserProvider
    {
        string GetUserId();

        string GetUsername();
    }
}
