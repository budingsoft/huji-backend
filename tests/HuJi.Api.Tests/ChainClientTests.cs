using HuJi.Api.Services;
using Xunit;

public class ChainClientTests
{
    [Fact(Skip = "integration — enable when CHAIN_RPC env var is set")]
    public async Task GetTeapotAsync_ReturnsNullForUnknownTokenId()
    {
        var rpc = Environment.GetEnvironmentVariable("CHAIN_RPC")!;
        var addr = Environment.GetEnvironmentVariable("HUJI_CONTRACT_ADDRESS")!;
        var client = new NethereumZGChainClient(rpc, addr, pk: null);
        var result = await client.GetTeapotAsync(99999);
        Assert.Null(result);
    }
}
