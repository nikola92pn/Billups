using Billups.Api.Endpoints;
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

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<PlayRequestValidator>();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Endpoints
app.MapEndpoints();

app.Run();