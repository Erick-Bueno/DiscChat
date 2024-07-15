namespace Name.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Channels : ControllerBase
    {
        private readonly IChannelService _channelService;

        public Channels(IChannelService channelService)
        {
            _channelService = channelService;
        }
        /// <summary>
        /// Buscar todos os canais de um servidor especifico
        /// </summary>
        /// <param name="externalIdServer">Id externo do server</param>
        /// <returns>Lista de canais de um servidor especifico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>

        [HttpGet("{externalIdServer}")]

        [ProducesResponseType(typeof(ResponseAllChannels), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseError),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllChannelsInServer([FromRoute] Guid externalIdServer)
        {
            var response = await _channelService.GetAllChannels(externalIdServer);
            return this.ToActionResult(response);
        }
        /// <summary>
        /// Criar um canal
        /// </summary>
        /// <remarks>
        /// {"channelName": "string","externalIdServer": "Guid"}
        /// </remarks>
        /// <param name="channel">Dados do canal</param>
        /// <returns>Canal</returns>
        /// <response code="201">Canal criado com sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCreateChannel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseError),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateChannel([FromBody] ChannelDto channel){
            var response = await _channelService.CreateChannel(channel);
            return this.ToActionResult(response);
        }
    }
}