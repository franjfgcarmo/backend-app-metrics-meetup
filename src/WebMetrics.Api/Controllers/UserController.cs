using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMetrics.Api.Auth.Entities;
using WebMetrics.Api.Auth.Sevices;

namespace WebMetrics.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Allow get all the users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /
        /// </remarks>
        /// <returns>IEnumerable&lt;User&gt;</returns>
        /// <response code="200">List of users</response>
        /// <response code="404">No users found </response> 
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet]
        [Route("")]
        public  IActionResult GetAll()
        {
            var result =  _userService.GetAll();
            if (result is null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Allow get an user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /1
        /// </remarks>
        /// <returns>User</returns>
        /// <response code="200">user</response>
        /// <response code="404">No users found </response> 
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
    }
}
