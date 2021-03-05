using System;

namespace DepsWebApp.Options
{
    /// <summary>
    /// Model of nbu client options
    /// </summary>
    public class NbuClientOptions
    {
        /// <summary>
        /// Base address of nbu client
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Model validation
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
}
