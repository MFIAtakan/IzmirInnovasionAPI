using Business.IServices;
using Business.Services;
using Data.Data;
using Data.Database;
using Domain.Entities;
using Domain.Models;
using Domain.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=.;Database=IzmirInno;TrustServerCertificate=True;Trusted_Connection=True;"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IRepository<UserOperations>, Repository<UserOperations>>();
builder.Services.AddScoped<IIdentityService, IdentityServiceImpl>();
builder.Services.AddScoped<IUserOperationsService, UserOperationsServiceImpl>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://localhost:44367") // Add the origin of your Razor Client application
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()  // Allow credentials (cookies, headers) to be sent with the request
        );
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






var app = builder.Build();

// Inside Configure method, before app.UseEndpoints
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
