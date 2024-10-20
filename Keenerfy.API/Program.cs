using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KeenerfyContext>();
builder.Services.AddTransient<DAL<Product>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/products", ([FromServices] DAL<Product> dal) => 
{
    return dal.Listar();
});

app.MapGet("/products/{name}", ([FromServices] DAL < Product > dal, string name) =>
{
    return dal.FindBy(a => a.Name.ToUpper().Equals(name.ToUpper()));
});

app.MapPost("/products", ([FromServices] DAL < Product > dal, [FromBody] Product product) =>
{
    dal.Adicionar(product);
    return Results.Ok(product);
});

app.Run();
