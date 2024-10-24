using Keenerfy.API.Endpoints;
using Keenerfy.API.Requests;
using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Keenerfy.Shared.Models.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KeenerfyContext>();
builder.Services.AddTransient<DAL<Product>>();
builder.Services.AddTransient<DAL<PurchaseOrder>>();
builder.Services.AddTransient<DAL<Sale>>();

IConfiguration _config = builder.Configuration;

var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


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
    options.AddDefaultPolicy(
    policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseCors();

app.PurchaseOrderEndpoints();
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
