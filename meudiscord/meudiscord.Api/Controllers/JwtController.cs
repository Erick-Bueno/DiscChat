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
        /// <summary>
        /// Gerar novos tokens jwt
        /// </summary>
        /// <remarks>{"refreshToken":"string"}</remarks>
        /// <param name="refreshToken">Token de atualização</param>
        /// <returns>Novos tokens</returns>
        /// <response code="200">Servidor criado com sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPatch]
        [ProducesResponseType(typeof(ResponseNewTokens),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InvalidRefreshToken),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerError),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var response = await _jwtService.RefreshToken(refreshToken);
            return this.ResponseRefreshTokenHelper(response);
        }
    }
}