using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMetrics.Api.Auth.Sevices;
using WebMetrics.Api.Auth.ViewModels;

namespace BalanceManagement.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Allow to a user authenticate
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo
        ///     {
        ///        "username": "user",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">User and password</param>
        /// <returns>userAuthenticated</returns>
        /// <response code="200">User Authenticated</response>
        /// <response code="400">User or password incorrect</response>  
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(WebMetrics.Api.Auth.Entities.User),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}
