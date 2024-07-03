namespace Name.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IAuthService _authService;

        public Auth(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var response = await _authService.Login(userLogin);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            var response = await _authService.Register(userRegister);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Created("localhost:7000", response);
        }
    }
}