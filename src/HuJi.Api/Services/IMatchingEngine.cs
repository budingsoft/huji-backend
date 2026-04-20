namespace HuJi.Api.Services;

public record MatchHit(string TokenId, float Similarity);

public interface IMatchingEngine
{
    void Index(string tokenId, float[] features);
    IReadOnlyList<MatchHit> Query(float[] features, int topK = 5);
}
