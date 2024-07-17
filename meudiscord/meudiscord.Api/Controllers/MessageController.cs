namespace Name.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Messages : ControllerBase
    {
        private readonly IMessageService _messageService;

        public Messages(IMessageService messageService)
        {
            _messageService = messageService;
        }
        /// <summary>
        ///  Buscar mensagens antigas em um canal
        /// </summary>
        /// <param name="externalIdChannel">Id externo do canal</param>
        /// <returns>Lista de mensagens antigas em um chat</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpGet("{externalIdChannel}")]
        [ProducesResponseType(typeof(ResponseGetOldMessages), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InvalidChannelError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetOldMessages([FromRoute] Guid externalIdChannel)
        {
            var response =  _messageService.GetOldMessages(externalIdChannel);  
            return this.ResponseGetOldMessagesHelper(response);
        }
        /// <summary>
        /// Deletar uma mensagem no canal
        /// </summary>
        /// <param name="externalIdChannel">Id externo do canal</param>
        /// <param name="externalIdMessage">Id externo da mensagem</param>
        /// <returns>Sucesso</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Dado não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [Authorize]
        [HttpDelete("{externalIdChannel}/{externalIdMessage}")]
        [ProducesResponseType(typeof(ResponseSuccessDefault),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ChannelNotFoundError),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(MessageNotFoundError),StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMessageInChannel([FromRoute] Guid externalIdChannel, [FromRoute] Guid externalIdMessage) {
            var response = await _messageService.DeleteMessageInChannel(externalIdChannel, externalIdMessage);
            return this.ResponseDeleteMessageInChannelHelper(response);
        }
        
    }
}