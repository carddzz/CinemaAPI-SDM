using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CinemaApi.Data;
using CinemaApi;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaContext>(Options =>
    Options.UseSqlite(builder.Configuration.GetConnectionString("CinemaContext") ?? throw new InvalidOperationException("Connection string 'CinemaContext' not found.")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCinemaEndpoints();

app.MapFilmeEndpoints();

app.Run();
