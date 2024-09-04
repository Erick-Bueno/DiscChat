namespace Name.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Guilds : ControllerBase
    {
        private readonly IGuildService _guildService;

        public Guilds(IGuildService guildService)
        {
            this._guildService = guildService;
        }

        /// <summary>
        /// Lista todos os servidores disponiveis
        /// </summary>
        /// <returns>Lista de servidores disponiveis</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Dado não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllGuilds), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoServersWereFoundError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGuilds()
        {
            var response = await _guildService.GetAllGuilds();
            return this.HandlerGetAllGuildsResponse(response);
        }
        /// <summary>
        /// Criar um servidor
        /// </summary>
        /// <remarks>
        /// {"serverName": "string","externalIdUser": "Guid"}
        /// </remarks>
        /// <param name="guild">Dados do servidor</param>
        /// <returns>Servidor</returns>
        /// <response code="201">Servidor criado com sucesso</response>
        /// <response code="404">Dado não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCreateGuild), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnableToCreateServerError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGuild([FromBody] GuildDto guild)
        {
            var response = await _guildService.CreateGuild(guild);
            return this.HandlerCreateGuildResponse(response);
        }
        /// <summary>
        /// Deletar um servidor
        /// </summary>
        /// <param name="externalIdServer">Id externo do servidor</param>
        /// <param name="externalIdUser">Id externo do usuário que esta tentando deletar o servidor</param>
        /// <returns>Sucesso</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Dado não encontrado</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpDelete("{externalIdServer}/{externalIdUser}")]
        [ProducesResponseType(typeof(ResponseSuccessDefault), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnableToDeleteServerError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TheServerDoesNotBelongToTheUserTryingToDeleItError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGuild([FromRoute] Guid externalIdServer, [FromRoute] Guid externalIdUser)
        {
            var deleteGuildDto = new DeleteGuildDto(externalIdServer, externalIdUser);
            var response = await _guildService.DeleteGuild(deleteGuildDto);

            return this.HandlerDeleteGuildResponse(response);
        }
        /// <summary>
        /// Retornar um unico servidor com base no seu external id
        /// </summary>
        /// <param name="externalIdServer">Id externo do servidor</param>
        /// <returns>Servidor</returns>]
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpGet("{externalIdServer}")]
        [ProducesResponseType(typeof(ResponseGetGuildByExternalId), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnableToDeleteServerError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGuildByExternalId([FromRoute] Guid externalIdServer)
        {
            var response = await _guildService.GetGuildByExternalId(externalIdServer);
            return this.HandlerGetGuildByExternalIdResponse(response);
        }
    }
}