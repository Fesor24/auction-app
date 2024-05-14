using AuctionService.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddSerilog();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseSerilogRequestLogging();

app.Run();
