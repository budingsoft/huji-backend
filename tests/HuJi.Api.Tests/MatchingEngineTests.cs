using HuJi.Api.Services;
using Xunit;

public class MatchingEngineTests
{
    [Fact]
    public void Query_ReturnsExactMatchAtTop()
    {
        var m = new InMemoryMatchingEngine();
        var v1 = new float[] { 1, 0, 0 };
        var v2 = new float[] { 0, 1, 0 };
        m.Index("t1", v1);
        m.Index("t2", v2);

        var hits = m.Query(new float[] { 1, 0, 0 });
        Assert.Equal("t1", hits[0].TokenId);
        Assert.True(hits[0].Similarity > 0.99);
    }

    [Fact]
    public void Query_ReturnsTopKSortedDesc()
    {
        var m = new InMemoryMatchingEngine();
        m.Index("a", new float[] { 1, 0, 0 });
        m.Index("b", new float[] { 0.9f, 0.1f, 0 });
        m.Index("c", new float[] { 0, 1, 0 });

        var hits = m.Query(new float[] { 1, 0, 0 }, topK: 2);
        Assert.Equal(2, hits.Count);
        Assert.Equal("a", hits[0].TokenId);
        Assert.Equal("b", hits[1].TokenId);
    }
}
