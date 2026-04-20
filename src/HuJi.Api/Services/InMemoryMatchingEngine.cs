namespace HuJi.Api.Services;

public class InMemoryMatchingEngine : IMatchingEngine
{
    private readonly Dictionary<string, float[]> _store = new();
    private readonly object _lock = new();

    public void Index(string tokenId, float[] features)
    {
        if (features.Length == 0) throw new ArgumentException("empty features");
        lock (_lock) { _store[tokenId] = (float[])features.Clone(); }
    }

    public IReadOnlyList<MatchHit> Query(float[] features, int topK = 5)
    {
        if (features.Length == 0) return Array.Empty<MatchHit>();
        var queryNorm = L2Norm(features);
        lock (_lock)
        {
            return _store
                .Select(kv => new MatchHit(kv.Key, CosineSimilarity(features, kv.Value, queryNorm)))
                .OrderByDescending(h => h.Similarity)
                .Take(topK)
                .ToArray();
        }
    }

    private static float L2Norm(float[] v)
    {
        double s = 0;
        for (int i = 0; i < v.Length; i++) s += v[i] * v[i];
        return (float)Math.Sqrt(s);
    }

    private static float CosineSimilarity(float[] q, float[] d, float qNorm)
    {
        if (q.Length != d.Length) return 0;
        double dot = 0, dn = 0;
        for (int i = 0; i < q.Length; i++) { dot += q[i] * d[i]; dn += d[i] * d[i]; }
        var denom = qNorm * Math.Sqrt(dn);
        return denom == 0 ? 0 : (float)(dot / denom);
    }
}
