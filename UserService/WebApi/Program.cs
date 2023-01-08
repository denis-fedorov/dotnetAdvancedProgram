using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebApi.Extensions;
using WebApi.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseRouteParameterTransformer())));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .ConfigureWebApi()
    .ConfigureInfrastructure();

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