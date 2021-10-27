using System.Collections.Generic;
using System.Linq;
using Models;

namespace HttpClient.Data.Implementations
{
    public class UserService : IUserService
    {
        private IList<User> _users;

        public UserService()
        {
            _users = new[]
            {
                new User("Andreas", "pass", 1),
                new User("Frederik", "pass", 2),
                new User("Troels", "pass", 3),
            }.ToList();
        }

        public User ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
    }
}