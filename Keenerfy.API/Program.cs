using Keenerfy.API.Endpoints;
using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KeenerfyContext>();
builder.Services.AddTransient<DAL<Product>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AllowAnonymous", policy => policy.RequireAssertion(context => true));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentityApiEndpoints<User>()
       .AddEntityFrameworkStores<KeenerfyContext>();

builder.Services.AddIdentityCore<User>(option =>
{
    option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.ProductsEndpoints();
app.SalesEndpoints();
app.UserEndpoints();

app.UseSwagger();
app.UseSwaggerUI();



app.MapIdentityApi<User>();



app.MapPost("/logout", async (SignInManager<User> signInManager, [FromBody] object empty) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});

app.Run();
