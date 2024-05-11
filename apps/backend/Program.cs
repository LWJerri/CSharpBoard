using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMvc();

builder.Services.AddControllers();

var dataSourceBuilder = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("DATABASE_URL"));

var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(dataSourceBuilder.ConnectionString).Options;

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(dataSourceBuilder.ConnectionString));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
  config.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "CSharpBoard API",
    Version = "v1",
    Description = "Detailed technical documentation describing all possible API endpoints for this project."
  });
});

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwagger();

app.UseReDoc(option =>
{
  option.SpecUrl = "/swagger/v1/swagger.json";
  option.RoutePrefix = string.Empty;
  option.DocumentTitle = "CSharpBoard API";
});

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();