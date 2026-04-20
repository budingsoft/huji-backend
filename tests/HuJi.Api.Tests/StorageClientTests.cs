using HuJi.Api.Services;
using System.Text;
using Xunit;

public class StorageClientTests
{
    [Fact(Skip = "integration — enable with 0G Storage endpoint configured")]
    public async Task UploadThenDownload_Roundtrip()
    {
        var endpoint = Environment.GetEnvironmentVariable("ZG_STORAGE_ENDPOINT")!;
        var c = new ZGStorageHttpClient(endpoint);
        var payload = Encoding.UTF8.GetBytes("hello huji");
        var hash = await c.UploadAsync(payload, "application/octet-stream");
        var roundtrip = await c.DownloadAsync(hash);
        Assert.Equal(payload, roundtrip);
    }
}
