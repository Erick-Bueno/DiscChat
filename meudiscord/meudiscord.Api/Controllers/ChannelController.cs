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

        [HttpGet("{externalIdServer}")]
        public async Task<IActionResult> GetAllChannelsInServer([FromRoute] Guid externalIdServer)
        {
            var response = await _channelService.GetAllChannels(externalIdServer);
            if (response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateChannel([FromBody] ChannelDto channel){
            var response = await _channelService.CreateChannel(channel);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Created("localhost:7000",response);
        }
    }
}