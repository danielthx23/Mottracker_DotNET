using DotNetEnv;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Mottracker.IoC;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Configuration;

// Carrega variáveis de ambiente do .env se não estiverem definidas
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ORACLE_HOST")))
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

// Explicitly add user secrets in Development to ensure they are read
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>(optional: true);
}

// Debug: show which configuration values are being used in Development
if (builder.Environment.IsDevelopment())
{
    var conn = builder.Configuration.GetConnectionString("SqlServer");
    Console.WriteLine($"[DEBUG] ConnectionString read from IConfiguration: {conn}");
    var jwtSecret = builder.Configuration["Jwt:SecretKey"];
    var masked = string.IsNullOrEmpty(jwtSecret) ? "<null>" : (jwtSecret.Length >6 ? jwtSecret.Substring(0,6) + "..." : jwtSecret);
    Console.WriteLine($"[DEBUG] Jwt:SecretKey (masked): {masked}");
}

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configurações via IoC
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddApiVersioningConfig();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHealthChecksConfig();

// Register Swagger examples from the main Mottracker assembly
builder.Services.AddSwaggerExamplesFromAssemblyOf(typeof(Program));

// Configure rate limiting in the web project
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("rateLimitePolicy", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(20);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Configure response compression in the web project
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseCors("AllowReactApp");

// Serve static files from wwwroot if the directory exists
var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (Directory.Exists(wwwrootPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(wwwrootPath),

        RequestPath = ""
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

// Health Check endpoint
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                exception = e.Value.Exception?.Message,
                duration = e.Value.Duration.ToString()
            }),
            totalDuration = report.TotalDuration.ToString()
        });
        await context.Response.WriteAsync(result);
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Expor Program para testes de integração
namespace Mottracker
{
    public partial class Program { }
}
