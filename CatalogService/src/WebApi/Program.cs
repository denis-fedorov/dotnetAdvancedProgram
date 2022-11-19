using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebApi.Middlewares;
using WebApi.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseRouteParameterTransformer())));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .ConfigureApplication()
    .ConfigureInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();