using Azure.Identity;
using Kicks.Azure.Services.KeyVault.Classe;
using Kicks.Azure.Services.KeyVault;
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

// Adicionando Azure Key Vault ao IConfiguration
var keyVaultUri = Environment.GetEnvironmentVariable("KEY_VAULT_URI") ?? "";
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());

var instrumentationKey = Environment.GetEnvironmentVariable("InstrumentationKey");
var ingestionEndpoint = Environment.GetEnvironmentVariable("IngestionEndpoint");
var liveEndpoint = Environment.GetEnvironmentVariable("LiveEndpoint");
var applicationId = Environment.GetEnvironmentVariable("ApplicationId");

var appInsightsConnectionString = $"InstrumentationKey={instrumentationKey};IngestionEndpoint={ingestionEndpoint};LiveEndpoint={liveEndpoint};ApplicationId={applicationId}";
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = appInsightsConnectionString;
});

// Pegando o segredo do Key Vault diretamente do IConfiguration
var secret = builder.Configuration["key-token-authentication"] ?? "";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<KicksDataContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IKeyVaultService, KeyVaultService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddSwaggerGen(options =>
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<KicksExceptionMiddleware>();
app.MapControllers();

app.Run();
