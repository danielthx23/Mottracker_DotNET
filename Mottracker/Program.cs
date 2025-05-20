using Microsoft.EntityFrameworkCore;
using Mottracker.Infrastructure.AppData;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Services;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {                   
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add DbContext and configure Oracle connection
string connectionString = builder.Configuration.GetConnectionString("Oracle");

if (string.IsNullOrWhiteSpace(connectionString))
{
    var oracleUser = Environment.GetEnvironmentVariable("ORACLE_USER");
    var oraclePassword = Environment.GetEnvironmentVariable("ORACLE_PASSWORD");
    var oracleHost = Environment.GetEnvironmentVariable("ORACLE_HOST");
    var oraclePort = Environment.GetEnvironmentVariable("ORACLE_PORT");
    var oracleSid = Environment.GetEnvironmentVariable("ORACLE_SID");

    if (!string.IsNullOrEmpty(oracleUser) && !string.IsNullOrEmpty(oraclePassword))
    {
        connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={oracleHost})(PORT={oraclePort})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={oracleSid})));User Id={oracleUser};Password={oraclePassword};";
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


// Register Repositories and Application Services
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

builder.Services.AddTransient<ICameraApplicationService, CameraApplicationService>();
builder.Services.AddTransient<IContratoApplicationService, ContratoApplicationService>();
builder.Services.AddTransient<IEnderecoApplicationService, EnderecoApplicationService>();
builder.Services.AddTransient<ILayoutPatioApplicationService, LayoutPatioApplicationService>();
builder.Services.AddTransient<IMotoApplicationService, MotoApplicationService>();
builder.Services.AddTransient<IPatioApplicationService, PatioApplicationService>();
builder.Services.AddTransient<IPermissaoApplicationService, PermissaoApplicationService>();
builder.Services.AddTransient<IQrCodePontoApplicationService, QrCodePontoApplicationService>();
builder.Services.AddTransient<ITelefoneApplicationService, TelefoneApplicationService>();
builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddTransient<IUsuarioPermissaoApplicationService, UsuarioPermissaoApplicationService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
