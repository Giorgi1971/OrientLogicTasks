using CredoProject.Core.Db;
using CredoProject.Core.Repositories;
using CredoProject.Core.Services;
using CredoProject.Core.Validations;
using CredoProject.Core.Calculates;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IATMServices, ATMService>();
builder.Services.AddTransient<IValidate, Validate>();
builder.Services.AddTransient<ICalculate, Calculate>();
builder.Services.AddTransient<ICardRepository, CardRepository>();

// Add services to the container.
builder.Services.AddDbContext<CredoDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CredoDbContext")));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

