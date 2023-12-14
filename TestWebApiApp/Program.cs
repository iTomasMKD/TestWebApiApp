using Cqrs.Hosts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Repository;
using TestWebApiApp.Core.Validators;
using TestWebApiApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://dummy.com") // Add your allowed origin(s)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(typeof(StartUp));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddOptionsWithValidateOnStart<CreateUserValidator>();
builder.Services.AddOptionsWithValidateOnStart<CreateUserValidator>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Implement this
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
