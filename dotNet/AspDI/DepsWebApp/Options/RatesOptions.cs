namespace DepsWebApp.Options
{
    /// <summary>
    /// Class of rates options
    /// </summary>
    public class RatesOptions
    {
        /// <summary>
        /// Base currency in program
        /// </summary>
        public string BaseCurrency { get; set; }
        
        /// <summary>
        /// Model validation
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
}
