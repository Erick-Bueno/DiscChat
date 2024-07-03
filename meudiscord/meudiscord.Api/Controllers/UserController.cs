namespace Name.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        private readonly IUserService _userService;

        public User(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{externalId}")]
        public async Task<IActionResult> FindUserAuthenticated([FromRoute] Guid externalId)
        {
            var response = await _userService.FindUserAuthenticated(externalId);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}