
using API.Middleware;
using Application.Core;
using Application.Recipes;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<CreateRecipe>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// adding DataContext as app service
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlite(connectionString)
);

// add MediatR as a service requires the assembly reference where to find it
builder.Services.AddMediatR(typeof(ListRecipes.Handler).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsAllowAnything",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});


var app = builder.Build();

// adding global custom exception handling earliest possible
app.UseMiddleware<ExceptionMiddleware>();

// check if there is already a working db otherwise create it upon starting app
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // use our DataContext added above to apply Migration code First
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration error occured");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsAllowAnything");

app.UseAuthorization();

app.MapControllers();


await app.RunAsync();
