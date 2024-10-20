using Keenerfy.API.Endpoints;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ProductsEndpoints();
app.SalesEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
