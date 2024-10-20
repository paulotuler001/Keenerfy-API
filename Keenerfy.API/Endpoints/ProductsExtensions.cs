using Keenerfy.API.Requests;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keenerfy.API.Endpoints;
public static class ProductsExtensions
{
    public static void ProductsEndpoints(this WebApplication app)
    {
        app.MapGet("/products", ([FromServices] DAL<Product> dal) =>
        {
            return dal.List();
        });

        app.MapGet("/products/{name}", ([FromServices] DAL<Product> dal, string name) =>
        {
            return dal.FindBy(a => a.Name.ToUpper().Equals(name.ToUpper()));
        });

        app.MapPost("/products", ([FromServices] DAL<Product> dal, [FromBody] ProductsRequest productRequest) =>
        {
            var product = new Product(productRequest.Name, productRequest.Code, productRequest.Description, productRequest.Price, productRequest.Link, productRequest.Stock_id);
            dal.Create(product);
            return Results.Ok(product);
        });

        app.MapPut("/products", ([FromServices] DAL<Product> dal, [FromBody] ProductsRequestEdit productRequestEdit) =>
        {
            var productToUpdate = dal.FindBy(a => a.Id.Equals(productRequestEdit.Id));
            if (productToUpdate is null)
            {
                return Results.NotFound();
            }
            dal.Update(productToUpdate);
            return Results.Ok(productToUpdate);
        });
    }
}
