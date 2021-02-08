using System.Text.Json.Serialization;

namespace RequestProcessor.App.Models
{
    public class RequestOptions : IRequestOptions, IResponseOptions
    {
        public RequestOptions()
        {
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("method")]
        public RequestMethod Method { get; set; }

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }
        
        [JsonPropertyName("body")]
        public string Body { get; set; }
        
        [JsonPropertyName("path")]
        public string Path { get; set; }
        
        [JsonIgnore]
        public bool IsValid {
            get
            {
                if (string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Path) || Method == RequestMethod.Undefined)
                    return false;
                
                return true;
            }
        }
    }
}