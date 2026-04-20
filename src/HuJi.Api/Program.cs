using HuJi.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMatchingEngine, InMemoryMatchingEngine>();
builder.Services.AddSingleton<IZGChainClient>(_ => new NethereumZGChainClient(
    builder.Configuration["Chain:Rpc"] ?? "",
    builder.Configuration["Chain:ContractAddress"] ?? "",
    builder.Configuration["Chain:MasterPrivateKey"]));

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));
app.Run();

public partial class Program { }
