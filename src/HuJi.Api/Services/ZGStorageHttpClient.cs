using System.Net.Http.Headers;

namespace HuJi.Api.Services;

public class ZGStorageHttpClient(string baseUrl, HttpClient? http = null) : IZGStorageClient
{
    private readonly HttpClient _http = http ?? new HttpClient { BaseAddress = new Uri(baseUrl) };

    public async Task<string> UploadAsync(ReadOnlyMemory<byte> data, string contentType, CancellationToken ct = default)
    {
        using var content = new ByteArrayContent(data.ToArray());
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        var resp = await _http.PostAsync("/upload", content, ct);
        resp.EnsureSuccessStatusCode();
        var body = await resp.Content.ReadAsStringAsync(ct);
        var hash = body.Trim('"', ' ', '\n');
        if (hash.StartsWith("{"))
            hash = System.Text.Json.JsonDocument.Parse(body).RootElement.GetProperty("root").GetString()!;
        return hash;
    }

    public async Task<byte[]> DownloadAsync(string hash, CancellationToken ct = default)
    {
        var resp = await _http.GetAsync($"/download/{hash}", ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadAsByteArrayAsync(ct);
    }
}
