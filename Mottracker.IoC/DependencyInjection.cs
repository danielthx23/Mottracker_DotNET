using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Services;
using Mottracker.Application.UseCases;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;
using Mottracker.Infrastructure.HealthCheck;
using Mottracker.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Mottracker.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // If running tests, use InMemory database to avoid requiring SQL Server
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? configuration["ASPNETCORE_ENVIRONMENT"];
        if (string.Equals(env, "Testing", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<ApplicationContext>(x => x.UseInMemoryDatabase("TestDb"));
            return services;
        }

        // Configuração de conexão com SQL Server
        string? connectionString = configuration.GetConnectionString("SqlServer")
            ?? Environment.GetEnvironmentVariable("SQLSERVER_CONNECTIONSTRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var sqlHost = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "localhost";
            var sqlPort = Environment.GetEnvironmentVariable("SQLSERVER_PORT") ?? "1433";
            var sqlDatabase = Environment.GetEnvironmentVariable("SQLSERVER_DATABASE") ?? "MottrackerDb";
            var sqlUser = Environment.GetEnvironmentVariable("SQLSERVER_USER");
            var sqlPassword = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD");
            var useIntegratedSecurity = string.Equals(Environment.GetEnvironmentVariable("SQLSERVER_INTEGRATED_SECURITY"), "true", StringComparison.OrdinalIgnoreCase);

            if (useIntegratedSecurity)
            {
                connectionString = $"Server={sqlHost},{sqlPort};Database={sqlDatabase};Integrated Security=true;TrustServerCertificate=true;";
            }
            else if (!string.IsNullOrEmpty(sqlUser) && !string.IsNullOrEmpty(sqlPassword))
            {
                connectionString = $"Server={sqlHost},{sqlPort};Database={sqlDatabase};User Id={sqlUser};Password={sqlPassword};TrustServerCertificate=true;";
            }
            else
            {
                // No SQL connection configured: fall back to in-memory DB to allow tests/local runs
                services.AddDbContext<ApplicationContext>(x => x.UseInMemoryDatabase("FallbackDb"));
                return services;
            }
        }

        services.AddDbContext<ApplicationContext>(x =>
        {
            x.UseSqlServer(connectionString, sqlOptions =>
            {
                // Enable retry on failure for transient faults (recommended for Azure SQL)
                sqlOptions.EnableRetryOnFailure(maxRetryCount:5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(60);
            });
        });

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddSingleton<IMlPredictionService, MlPredictionService>();

        // Repositórios
        services.AddTransient<ICameraRepository, CameraRepository>();
        services.AddTransient<IContratoRepository, ContratoRepository>();
        services.AddTransient<IEnderecoRepository, EnderecoRepository>();
        services.AddTransient<ILayoutPatioRepository, LayoutPatioRepository>();
        services.AddTransient<IMotoRepository, MotoRepository>();
        services.AddTransient<IPatioRepository, PatioRepository>();
        services.AddTransient<IPermissaoRepository, PermissaoRepository>();
        services.AddTransient<IQrCodePontoRepository, QrCodePontoRepository>();
        services.AddTransient<ITelefoneRepository, TelefoneRepository>();
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IUsuarioPermissaoRepository, UsuarioPermissaoRepository>();

        // UseCases
        services.AddTransient<ICameraUseCase, CameraUseCase>();
        services.AddTransient<IContratoUseCase, ContratoUseCase>();
        services.AddTransient<IEnderecoUseCase, EnderecoUseCase>();
        services.AddTransient<ILayoutPatioUseCase, LayoutPatioUseCase>();
        services.AddTransient<IMotoUseCase, MotoUseCase>();
        services.AddTransient<IPatioUseCase, PatioUseCase>();
        services.AddTransient<IPermissaoUseCase, PermissaoUseCase>();
        services.AddTransient<IQrCodePontoUseCase, QrCodePontoUseCase>();
        services.AddTransient<ITelefoneUseCase, TelefoneUseCase>();
        services.AddTransient<IUsuarioUseCase, UsuarioUseCase>();
        services.AddTransient<IUsuarioPermissaoUseCase, UsuarioPermissaoUseCase>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        // MVC + Views
        services.AddControllersWithViews()
            .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Swagger
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.ExampleFilters();

            // Configuração para versionamento
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mottracker API",
                Version = "v1",
                Description = "API RESTful para gerenciamento de motos e pátios da Mottu",
                Contact = new OpenApiContact
                {
                    Name = "Mottracker Team",
                    Email = "support@mottracker.com"
                }
            });

            // Configuração JWT no Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        // Swagger examples should be registered in the web project (Mottracker)

        return services;
    }

    public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("version"),
                new HeaderApiVersionReader("X-Version"),
                new UrlSegmentApiVersionReader()
            );
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSecretKey = configuration["Jwt:SecretKey"]
            ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
            ?? "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm";
        var jwtIssuer = configuration["Jwt:Issuer"] ?? "MottrackerAPI";
        var jwtAudience = configuration["Jwt:Audience"] ?? "MottrackerClient";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddHealthChecksConfig(this IServiceCollection services)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var hc = services.AddHealthChecks();

        if (!string.Equals(env, "Testing", StringComparison.OrdinalIgnoreCase))
        {
            hc.AddCheck<DatabaseHealthCheck>("database", tags: new[] { "database", "sql", "oracle" });
        }

        hc.AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "self" });

        return services;
    }
}

