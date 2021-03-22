using System;
using System.Threading.Tasks;
using DepsWebApp.Context;
using DepsWebApp.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserService(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for register
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>Bool value, true id registered, another - false</returns>
        /// <exception cref="ArgumentNullException">When some arguments is null</exception>
        public async Task<bool> RegisterAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            
            await _context.Users.AddAsync(new User(login, password));
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<User> GetUser(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
            if (user != null && user.Password == password)
            {
                return user;
            }
            
            return null;
        }
    }
}