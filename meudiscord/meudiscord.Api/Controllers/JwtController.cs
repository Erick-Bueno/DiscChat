namespace Name.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Jwt : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public Jwt(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPatch]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var response = await _jwtService.RefreshToken(refreshToken);
            return this.ResponseRefreshTokenHelper(response);
        }
    }
}