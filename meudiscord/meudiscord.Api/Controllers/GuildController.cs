namespace Name.Controllers
{

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

        [HttpGet]
        public async Task<IActionResult> GetAllGuilds()
        {
            var response = await _guildService.GetAllGuilds();
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGuild([FromBody] GuildDto guild){
            var response = await _guildService.CreateGuild(guild);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Created("localhost:7000", response);
        }
        [HttpDelete("{externalIdServer}/{externalIdUser}")]
        public async Task<IActionResult> DeleteGuild([FromRoute] Guid externalIdServer, [FromRoute] Guid externalIdUser){
            var deleteGuildDto = new DeleteGuildDto(externalIdServer, externalIdUser);
            var response = await _guildService.DeleteGuild(deleteGuildDto);
            if(response is ResponseError){
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}