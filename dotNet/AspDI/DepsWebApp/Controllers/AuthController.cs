using System;
using DepsWebApp.Contracts;
using DepsWebApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Controller for registration user
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Post request for register user
        /// </summary>
        /// <param name="user">User for register</param>
        /// <exception cref="NotImplementedException">Method need implementation</exception>
        [HttpPost("register")]
        [TypeFilter(typeof(ExceptionFilter))]
        public ActionResult Register([FromBody] UserForRegister user)
        {
            throw new NotImplementedException();
        }
    }
}