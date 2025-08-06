using Product.Application;
using Product.Infrastructure;
using Product.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    // API Controllers
    services.AddControllers();
    
    // Swagger/OpenAPI
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Product Service API",
            Version = "v1",
            Description = "Street Food Product Microservice"
        });
    });

    // CORS
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    // Add Application Layer
    services.AddApplication();
    
    // Add Infrastructure Layer
    services.AddInfrastructure(configuration);

    // Health Checks
    services.AddHealthChecks()
        .AddDbContextCheck<ProductDbContext>();
}

var app = builder.Build();

// Configure the HTTP request pipeline
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service API V1");
            c.RoutePrefix = string.Empty; // Swagger at root
        });
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseAuthorization();
    app.MapControllers();
    app.MapHealthChecks("/health");

    // Seed Database
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        
        if (app.Environment.IsDevelopment())
        {
            await context.Database.EnsureCreatedAsync();
            await ProductDbContextSeed.SeedAsync(context);
        }
    }
}

app.Run();