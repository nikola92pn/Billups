using Billups.Api.Endpoints;
using Billups.Api.Extensions;
using Billups.Api.Middlewares;
using Billups.Api.Validation;
using Billups.Application.Extensions;
using Billups.Domain.Extensions;
using Billups.Infrastructure.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

builder.Services.AddHttpContextAccessor();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<PlayRequestValidator>();

// CORS Policies
builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetSection("AllowedHosts").Get<string>() ?? "*";
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middlewares
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Endpoints
app.MapChoicesEndpoints();
app.MapGameEndpoints();
app.MapHistoryEndpoints();

app.Run();