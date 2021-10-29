using Models;

namespace WebClient.Data
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}