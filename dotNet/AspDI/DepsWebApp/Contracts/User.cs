using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DepsWebApp.Contracts
{
    /// <summary>
    /// User model for register
    /// </summary>
    public class User
    {
        /// <summary>
        /// User login
        /// </summary>
        [JsonPropertyName("login")]
        [MinLength(6)]
        public string Login { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        [JsonPropertyName("password")]
        [MinLength(6)]
        public string Password { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}