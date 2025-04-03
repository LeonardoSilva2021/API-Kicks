using Kicks.Data.Database;
using Kicks.Services.Exceptions.Middleware;
using Kicks.Services.Services.Auth;
using Kicks.Services.Services.Auth.Classes;
using Kicks.Services.Services.Categoria;
using Kicks.Services.Services.Categoria.Classe;
using Kicks.Services.Services.Produto;
using Kicks.Services.Services.Produto.Classe;
using Kicks.Services.Services.Usuario;
using Kicks.Services.Services.Usuario.Classe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var instrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATION_KEY");
var ingestionEndpoint = Environment.GetEnvironmentVariable("APPINSIGHTS_INGESTION_ENDPOINT");
var liveEndpoint = Environment.GetEnvironmentVariable("APPINSIGHTS_LIVE_ENDPOINT");
var applicationId = Environment.GetEnvironmentVariable("APPINSIGHTS_APPLICATION_ID");

var appInsightsConnectionString = $"InstrumentationKey={instrumentationKey};IngestionEndpoint={ingestionEndpoint};LiveEndpoint={liveEndpoint};ApplicationId={applicationId}";

Console.WriteLine("Conexão com o Application Insights configurada.");

// Configurando o Application Insights com a string de conexão combinada
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = appInsightsConnectionString;
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<KicksDataContext>();

builder.Services.AddScoped<KeyVault>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = new KeyVault();
        var secret = key.GetSecret("key-token-authentication");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.Value)),
        };
    });

builder.Services
    .AddCors(options => {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<KicksExceptionMiddleware>();

app.MapControllers();

app.Run();
