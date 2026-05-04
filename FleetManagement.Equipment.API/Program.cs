using FleetManagement.Equipment.Infrastructure;
using FleetManagement.Equipment.Application;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
  var appConfigConnectionString = builder.Configuration["Azure:AppConfig"];
  var appConfigLabel = builder.Configuration["Azure:AppConfigLabel"];

  if (!string.IsNullOrEmpty(appConfigConnectionString))
  {
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
      options.Connect(appConfigConnectionString)
                 .Select("*")
                 .Select("*", "dev");
    });
  }
}

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlDatabase");

if (string.IsNullOrWhiteSpace(sqlConnectionString))
  throw new ArgumentNullException(nameof(sqlConnectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(sqlConnectionString);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
