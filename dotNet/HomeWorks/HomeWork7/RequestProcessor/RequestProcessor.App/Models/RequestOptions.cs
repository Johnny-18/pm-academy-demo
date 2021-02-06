namespace RequestProcessor.App.Models
{
    public class RequestOptions : IRequestOptions, IResponseOptions
    {
        public RequestOptions(string name, string address, string contentType, string body, string path, RequestMethod method)
        {
            Name = name;
            Address = address;
            ContentType = contentType;
            Body = body;
            Path = path;
            Method = method;
        }

        public string Name { get; }
        
        public string Address { get; }

        public RequestMethod Method { get; }

        public string ContentType { get; }
        
        public string Body { get; }
        
        public string Path { get; }
        
        public bool IsValid { get; }
    }
}