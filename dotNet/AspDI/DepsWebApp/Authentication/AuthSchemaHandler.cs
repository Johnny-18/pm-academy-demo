using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DepsWebApp.Authentication
{
    /// <summary>
    /// Custom auth handler for schema
    /// </summary>
    public class AuthSchemaHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Schema options</param>
        /// <param name="logger">Logger</param>
        /// <param name="encoder">Encoder</param>
        /// <param name="clock">System clock</param>
        /// <param name="userService">User service</param>
        public AuthSchemaHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        /// <summary>
        /// Method for handling auth request
        /// </summary>
        /// <returns>Authenticate result, no result if user not found or header is incorrect, success if all is good</returns>
#pragma warning disable 1998
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
#pragma warning restore 1998
        {
            string authHeader = null;
            if (Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                authHeader = Request.Headers[HeaderNames.Authorization].FirstOrDefault();
            }

            if (string.IsNullOrEmpty(authHeader))
                return AuthenticateResult.NoResult();

            try
            {
                if (authHeader.Contains("BasicAuthentication"))
                {
                    authHeader = authHeader.Replace("BasicAuthentication", "");
                }
                
                var credentialBytes = Convert.FromBase64String(authHeader.Trim());
                var credentials = Encoding.ASCII.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var login = credentials[0];
                var password = credentials[1];
                
                var user = await _userService.GetUser(login, password);
                if(user == null)
                    return AuthenticateResult.NoResult();

                var claims = new[] {
                    new Claim(ClaimTypes.Name, user.Login)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    
                return AuthenticateResult.Success(ticket);
            }
            catch(Exception)
            {
                return AuthenticateResult.NoResult();
            }
        }
    }
}