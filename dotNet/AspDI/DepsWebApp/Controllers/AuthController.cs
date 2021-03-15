using System.Threading.Tasks;
using DepsWebApp.Contracts;
using DepsWebApp.Filters;
using DepsWebApp.Services;
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
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService">User service</param>
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Post request for register user
        /// </summary>
        /// <param name="user">User for register</param>
        [HttpPost("register")]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (await _userService.GetUser(user.Login, user.Password) != null)
                return BadRequest();

            if (await _userService.RegisterAsync(user.Login, user.Password))
            {
                return Ok();
            }
            
            return BadRequest();
        }
    }
}