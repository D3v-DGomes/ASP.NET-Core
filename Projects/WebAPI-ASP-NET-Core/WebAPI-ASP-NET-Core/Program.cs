using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebAPI_ASP_NET_Core.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");   // String de conexão
builder.Services.AddDbContext<AppDbContext>(options =>      // Contexto do Entity Framework Core
                                options.UseMySql(mySqlConnection, 
                                ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Adicionando SwaggerUI:
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/openapi/v1.json", "weather api"));

    // Adicionando Scalar (alternativa do SwaggerUI):
    //app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
