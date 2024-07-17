namespace Name.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using OneOf.Types;

    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IAuthService _authService;

        public Auth(IAuthService authService)
        {
            this._authService = authService;
        }
        /// <summary>
        /// Efetuar login
        /// </summary>
        /// <remarks>
        /// {"email": "string",
        ///"password": "string"}
        /// </remarks>
        /// <param name="userLogin">Dados de login do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="404">Dado não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseAuth), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserNotRegisteredError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IncorrectPasswordError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var response = await _authService.Login(userLogin);
            return this.ResponseAuthHelper(response);
        }
        /// <summary>
        /// Efetuar registro
        /// </summary>
        /// <remarks>
        /// {"name": "string","email": "string","password": "string"}
        /// </remarks>
        /// <param name="userRegister">Dados de registro do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="201">Usuário cadastrado com sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseAuth), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmailIsAlreadyRegisteredError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            var response = await _authService.Register(userRegister);
            return this.ResponseRegisterHelper(response);
        }
    }
}