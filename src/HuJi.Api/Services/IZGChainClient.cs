namespace HuJi.Api.Services;

public record TeapotOnChain(
    ulong TokenId,
    string FeatureHash,
    string PhotoHash,
    string Craftsman,
    ulong MintedAt,
    ushort BatchNumber,
    string ClayType,
    ushort FiringTemp,
    string CurrentOwner);

public interface IZGChainClient
{
    Task<TeapotOnChain?> GetTeapotAsync(ulong tokenId, CancellationToken ct = default);
    Task<string> MintTeapotAsync(string featureHashHex, string photoHashHex, ushort batch, string clay, ushort firingTemp, CancellationToken ct = default);
    Task VerifyCraftsmanAsync(string craftsmanAddress, string profileURI, CancellationToken ct = default);
}
