using Core.WebApi.Middlewares.ExceptionHandling;
using ECommerce;
using ECommerce.Api.Core;
using ECommerce.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.Api", Version = "v1" });
    })
    .AddCoreServices(builder.Configuration)
    .AddECommerceModule()
    .AddEventStoreDBSubscriptionToAll(); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger()
    .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Api v1"))
    .UseMiddleware(typeof(ExceptionHandlingMiddleware));

if (app.Environment.IsDevelopment())
{
    app.Services.UseECommerceModule();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();

app.MapControllers();

app.Run();
