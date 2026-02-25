using AspNetCoreRateLimit;
using Serilog;
using TodoApi.API;
using TodoApi.API.Middleware;
using TodoApi.Application;
using TodoApi.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


// ── Serilog bootstrap (captures startup errors too) ──────────────────
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateBootstrapLogger();


builder.Host.UseSerilog((context, services, config) =>
    config.ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext());


// ── Register layers ──────────────────────────────────────────────────
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);


// ── Health checks ────────────────────────────────────────────────────
builder.Services.AddHealthChecks();


var app = builder.Build();


// ── Middleware pipeline ──────────────────────────────────────────────
app.UseMiddleware<ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");


app.Run();


// Required for WebApplicationFactory in integration tests
public partial class Program { }
