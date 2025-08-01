using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Model.Request.Auth;
using Practice.Services.Interfaces;

namespace Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequest loginUserRequest)
        {
            return Ok(await authService.Login(loginUserRequest));
        }

        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRequestModel userRequestModel)
        {
            return Ok(await authService.AddUser(userRequestModel));
        }


    }
}
