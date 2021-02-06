namespace RequestProcessor.App.Models
{
    public class Response : IResponse
    {
        public Response(bool handled, int code, string content)
        {
            Handled = handled;
            Code = code;
            Content = content;
        }

        public bool Handled { get; }
        
        public int Code { get; }
        
        public string Content { get; }
    }
}