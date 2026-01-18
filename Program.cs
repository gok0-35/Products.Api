using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Middlewares;
using Products.Api.Repositories.Abstractions;
using Products.Api.Repositories.EfCore;
using Products.Api.Responses;
using Products.Api.Services;
using Products.Api.Services.Abstractions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// SERILOG
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

// Add services to the container.
builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:ProductsDb"]);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var response = new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Message = "Request validation failed",
                TraceId = context.HttpContext.TraceIdentifier,
                Timestamp = DateTime.UtcNow,
                Errors = errors
            };

            return new BadRequestObjectResult(response);
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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
