using IdentityServer.Data;
using IdentityServer.Models;
using LibraryAPI.Application.Contracts.ServiceContracts;
using LibraryAPI.Application.Services;
using LibraryAPI.Extensions;
using LibraryAPI.Infrastructure;
using LibraryAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("LibraryDatabase");
builder.Services.AddDbContext<IdentityServerDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServerDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Add it to the request header as 'X-Api-Key'.",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKeyScheme",
                Name = "X-Api-Key",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();
await SeedData.InitializeAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {

        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Api v1");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleWare();
app.UseApiKeyMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
