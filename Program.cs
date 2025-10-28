using dotnetLearn.Data;
using dotnetLearn.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
// added global exception handler 
builder.Services.AddExceptionHandler< GlobalExceptionHandler > ();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// middleware example 
app.Use(async (context, next) =>
{
    // Before next middleware
    Console.WriteLine($"Request: {context.Request.Path}");

    await next(); // Call next middleware

    // After next middleware
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});
// minimal api routing example 
app.MapGet("/", () => 
{
    return Results.Ok("hello world");
});

app.MapControllers();
app.Run();