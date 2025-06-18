using Billups.Api.Endpoints;
using Billups.Application.Extensions;
using Billups.Domain.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region Services

builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();

#endregion

#region Swagger

builder.Services.AddSwaggerGen(config =>
    config.SwaggerDoc("Billups", new OpenApiInfo()
    {
        Title = "Billups API",
        Version = "v1",
        Description = "Billups API"
    }));

#endregion

#region Endpoints

app.MapEndpoints();
app.MapGet("/", () => "Hello World!");

#endregion

app.Run();