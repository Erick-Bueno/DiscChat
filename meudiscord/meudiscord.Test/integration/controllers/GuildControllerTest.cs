using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

[Collection("database")]
public class GuildControllerTest : IClassFixture<MeuDiscordFactory>, IClassFixture<MeuDiscordFactoryWithData>
{
    private readonly MeuDiscordFactory _factory;
    private readonly MeuDiscordFactoryWithData _factoryWithData;

    public GuildControllerTest(MeuDiscordFactory factory, MeuDiscordFactoryWithData factoryWithData)
    {
        _factory = factory;
        _factoryWithData = factoryWithData;
    }

    [Fact]
    public async void should_return_badrequest_no_guilds_found_when_get_all_guilds()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Guilds");
        var responseContent = await response.Content.ReadAsStringAsync();
        var getAllGuildsResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, getAllGuildsResponse.status);
        Assert.Equal("Nenhum servidor foi encontrado", getAllGuildsResponse.message);
    }
    [Fact]
    public async void should_return_ok_when_get_all_guilds()
    {
        var client = _factoryWithData.CreateClient();
        var response = await client.GetAsync("api/Guilds");
        var responseContent = await response.Content.ReadAsStringAsync();
        var getAllGuildsResponse = JsonConvert.DeserializeObject<ResponseAllGuilds>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, getAllGuildsResponse.status);
        Assert.Equal("Guildas encontradas", getAllGuildsResponse.message);

    }
 /*    [Fact]
    public async void should_return_badrequest_unable_to_create_the_server()
    {
        var client = _factory.CreateClient();
        var request = new GuildDto();
        var response = await client.PostAsJsonAsync("api/Guilds", request);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    } */
}