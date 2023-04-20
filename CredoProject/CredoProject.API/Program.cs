using Microsoft.EntityFrameworkCore;
using CredoProject.Core.Db;
using CredoProject.Core.Services;
using CredoProject.Core.Validations;
using CredoProject.Core.Repositories;
using System.Text.Json.Serialization;
using CredoProject.API.Auth;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AuthConfigurator.Configure(builder);
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddDbContext<CredoDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CredoDbContext")));

builder.Services
    .AddTransient<IAuthService, AuthService>()
    .AddTransient<IReportsService, ReportsService>()
    .AddTransient<ISendEmailRequestRepository, SendEmailRequestRepository>()
    .AddTransient<IReportsRepository, ReportsRepository>()
    .AddTransient<ICoreServices, CoreServices>()
    .AddTransient<ICardRepository, CardRepository>()
    .AddTransient<IValidate, Validate>()
    .AddTransient<IBankRepository, BankRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

