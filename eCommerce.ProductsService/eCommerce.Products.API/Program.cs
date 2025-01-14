using Carter;
using eCommerce.Products.API.Middlewares;
using eCommerce.Products.BLL;
using eCommerce.Products.DAL;
using eCommerce.Products.DAL.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDal(builder.Configuration);
builder.Services.AddBll();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce Products API", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCarter();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x =>
    {
        x.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

await app.InitialiseDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "eCommerce Products API v1"); });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.MapCarter();

app.Run();