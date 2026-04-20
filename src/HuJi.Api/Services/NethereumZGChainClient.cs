using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace HuJi.Api.Services;

public class NethereumZGChainClient(string rpcUrl, string contractAddress, string? pk) : IZGChainClient
{
    private readonly Web3 _web3 = pk is null
        ? new Web3(rpcUrl)
        : new Web3(new Account(pk), rpcUrl);
    private readonly string _contract = contractAddress;

    public Task<TeapotOnChain?> GetTeapotAsync(ulong tokenId, CancellationToken ct = default)
        => throw new NotImplementedException("bound in Task 2.1 after contract ABI is stable");

    public Task<string> MintTeapotAsync(string featureHashHex, string photoHashHex, ushort batch, string clay, ushort firingTemp, CancellationToken ct = default)
        => throw new NotImplementedException("bound in Task 2.1");

    public Task VerifyCraftsmanAsync(string craftsmanAddress, string profileURI, CancellationToken ct = default)
        => throw new NotImplementedException("bound in Task 2.1");
}
