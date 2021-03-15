using System;
using System.Threading.Tasks;
using DepsWebApp.Contracts;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Account service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>Return user account or null if login already taken</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null</exception>
        Task<bool> RegisterAsync(string login, string password);

        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>User or null if not found</returns>
        Task<User> GetUser(string login, string password);
    }
}