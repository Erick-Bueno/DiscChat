namespace Name.Controllers
{

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

        [HttpGet("{externalIdChannel}")]
        public async Task<IActionResult> GetOldMessages([FromRoute] Guid externalIdChannel)
        {
            var response =  _messageService.GetOldMessages(externalIdChannel);  
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{externalIdChannel}/{externalIdMessage}")]
        public async Task<IActionResult> DeleteMessageInChannel([FromRoute] Guid externalIdChannel, [FromRoute] Guid externalIdMessage) {
            var response = await _messageService.DeleteMessageInChannel(externalIdChannel, externalIdMessage);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
        
    }
}