using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Interfaces;
using Mottracker.Application.Interfaces;
using Mottracker.Application.UseCases;
using Mottracker.Infrastructure.AppData;
using Mottracker.Infrastructure.Data.Repositories;
using DotNetEnv;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Filters;

// Carrega variáveis de ambiente do .env se não estiverem definidas
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ORACLE_HOST")))
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

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

// Configuração de conexão com Oracle
string connectionString = builder.Configuration.GetConnectionString("Oracle");

if (string.IsNullOrWhiteSpace(connectionString))
{
    var oracleUser = Environment.GetEnvironmentVariable("ORACLE_USER");
    var oraclePassword = Environment.GetEnvironmentVariable("ORACLE_PASSWORD");
    var oracleHost = Environment.GetEnvironmentVariable("ORACLE_HOST") ?? "localhost";
    var oraclePort = Environment.GetEnvironmentVariable("ORACLE_PORT") ?? "1521";
    var oracleServiceName = Environment.GetEnvironmentVariable("ORACLE_SERVICE_NAME");
    var oracleSid = Environment.GetEnvironmentVariable("ORACLE_SID");

    if (!string.IsNullOrEmpty(oracleUser) && !string.IsNullOrEmpty(oraclePassword))
    {
        string connectDataPart;

        if (!string.IsNullOrEmpty(oracleServiceName))
        {
            connectDataPart = $"(SERVICE_NAME={oracleServiceName})";
        }
        else if (!string.IsNullOrEmpty(oracleSid))
        {
            connectDataPart = $"(SID={oracleSid})";
        }
        else
        {
            connectDataPart = "(SID=XE)";
        }

        connectionString =
            $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={oracleHost})(PORT={oraclePort})))(CONNECT_DATA=(SERVER=DEDICATED){connectDataPart}));User Id={oracleUser};Password={oraclePassword};";
    }
    else
    {
        throw new Exception("Nenhuma string de conexão Oracle configurada e variáveis de ambiente insuficientes.");
    }
}

builder.Services.AddDbContext<ApplicationContext>(x =>
{
    x.UseOracle(connectionString);
});

// MVC + Views
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Presentation/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Presentation/Views/Shared/{0}.cshtml");
    });

// Repositórios
builder.Services.AddTransient<ICameraRepository, CameraRepository>();
builder.Services.AddTransient<IContratoRepository, ContratoRepository>();
builder.Services.AddTransient<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddTransient<ILayoutPatioRepository, LayoutPatioRepository>();
builder.Services.AddTransient<IMotoRepository, MotoRepository>();
builder.Services.AddTransient<IPatioRepository, PatioRepository>();
builder.Services.AddTransient<IPermissaoRepository, PermissaoRepository>();
builder.Services.AddTransient<IQrCodePontoRepository, QrCodePontoRepository>();
builder.Services.AddTransient<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioPermissaoRepository, UsuarioPermissaoRepository>();

// UseCases
builder.Services.AddTransient<ICameraUseCase, CameraUseCase>();
builder.Services.AddTransient<IContratoUseCase, ContratoUseCase>();
builder.Services.AddTransient<IEnderecoUseCase, EnderecoUseCase>();
builder.Services.AddTransient<ILayoutPatioUseCase, LayoutPatioUseCase>();
builder.Services.AddTransient<IMotoUseCase, MotoUseCase>();
builder.Services.AddTransient<IPatioUseCase, PatioUseCase>();
builder.Services.AddTransient<IPermissaoUseCase, PermissaoUseCase>();
builder.Services.AddTransient<IQrCodePontoUseCase, QrCodePontoUseCase>();
builder.Services.AddTransient<ITelefoneUseCase, TelefoneUseCase>();
builder.Services.AddTransient<IUsuarioUseCase, UsuarioUseCase>();
builder.Services.AddTransient<IUsuarioPermissaoUseCase, UsuarioPermissaoUseCase>();

// Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "rateLimitePolicy", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(20);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Response Compression
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Presentation", "wwwroot")),
    RequestPath = ""
});

app.UseRouting();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
