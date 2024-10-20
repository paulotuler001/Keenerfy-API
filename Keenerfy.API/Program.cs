using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/", () => 
{
    var dal = new DAL<Product>(new KeenerfyContext());
    return dal.Listar();
});

app.Run();
