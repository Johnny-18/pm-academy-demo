using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Contracts;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Implements <see cref="IUserService"/>>
    /// </summary>
    public class UserServiceInMemory : IUserService
    {
        private readonly ConcurrentDictionary<string, User> _users =
            new ConcurrentDictionary<string, User>();

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        /// <inheritdoc />
        public async Task<bool> RegisterAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            var release = await _semaphore.WaitAsync(1000);
            try
            {
                return _users.TryAdd(login, new User(login, password));
            }
            finally
            {
                if(release) 
                    _semaphore.Release();
            }
        }

        /// <inheritdoc />
        public async Task<User> GetUser(string login, string password)
        {
            var release = await _semaphore.WaitAsync(1000);
            try
            {
                _users.TryGetValue(login, out var user);

                if (user != null && user.Password == password)
                {
                    return user;
                }
                
                return null;
            }
            finally
            {
                if(release) 
                    _semaphore.Release();
            }
        }
    }
}