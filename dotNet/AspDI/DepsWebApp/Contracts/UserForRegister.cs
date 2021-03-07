using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DepsWebApp.Contracts
{
    /// <summary>
    /// User model for register
    /// </summary>
    public class UserForRegister
    {
        /// <summary>
        /// User login
        /// </summary>
        [JsonPropertyName("login")]
        public string Login { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}