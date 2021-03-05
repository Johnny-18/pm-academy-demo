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
        [Required]
        public string Login { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
}