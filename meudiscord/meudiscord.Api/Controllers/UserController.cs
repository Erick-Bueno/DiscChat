namespace Name.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Busca dados do usuário autenticado
        /// </summary>
        /// <param name="externalId">Id externo do usuário autenticado</param>
        /// <returns>Dados do usuário autenticado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Dado não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpGet("{externalId}")]
        [ProducesResponseType(typeof(ResponseUserData),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserNotFoundError),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(InternalServerError),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FindUserAuthenticated([FromRoute] Guid externalId)
        {
            var response = await _userService.FindUserAuthenticated(externalId);
            return this.HandlerFindUserAuthenticatedResponse(response);
        }
    }
}