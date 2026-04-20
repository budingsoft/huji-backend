namespace HuJi.Api.Services;

public interface IZGStorageClient
{
    Task<string> UploadAsync(ReadOnlyMemory<byte> data, string contentType, CancellationToken ct = default);
    Task<byte[]> DownloadAsync(string hash, CancellationToken ct = default);
}
