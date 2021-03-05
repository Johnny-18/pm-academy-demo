using System.Text.Json.Serialization;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Model for exception result in filter
    /// </summary>
    public class ExceptionResult
    {
        /// <summary>
        /// Status code of exception
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        
        /// <summary>
        /// Message of exception
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}