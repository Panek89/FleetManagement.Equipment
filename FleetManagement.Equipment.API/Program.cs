using FleetManagement.Equipment.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlDatabase");
if (string.IsNullOrWhiteSpace(sqlConnectionString))
  throw new ArgumentNullException(nameof(sqlConnectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(sqlConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
