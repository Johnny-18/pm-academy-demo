using System.Text.Json.Serialization;

namespace DepsWebApp.Tests
{
    public class Settings
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
        
        [JsonPropertyName("ratesApi")]
        public string RatesApi { get; set; }
        
        [JsonPropertyName("authApi")]
        public string AuthApi { get; set; }
    }
}