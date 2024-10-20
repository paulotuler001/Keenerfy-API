using Keenerfy.API.Requests;
using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keenerfy.API.Endpoints;
public static class SalesExtensions
{
    public static void SalesEndpoints(this WebApplication app)
    {
        app.MapGet("/sales", ([FromServices] DAL<Sale> dal) =>
        {
            return dal.List();
        });

        app.MapGet("/sales/{Id}", ([FromServices] DAL<Sale> dal, int Id) =>
        {
            return dal.FindBy(a => a.Id == Id);
        });

        app.MapPost("/sales", ([FromServices] DAL<Sale> dal, SalesRequest salesRequest) =>
        {
            DAL<Product> dalProduct = new DAL<Product>(new KeenerfyContext());

            Product productToBeRelated = dalProduct.FindBy(prod => prod.Code.Equals(salesRequest.productRequest.Code));

            var sales = new Sale(salesRequest.Date, salesRequest.Quantity, productToBeRelated);
            dal.Create(sales);
            return Results.Ok(sales);
        });

        app.MapDelete("/sales/{Id}", ([FromServices] DAL<Sale> dal, int Id) =>
        {
            Sale saleToBeDeleted = dal.FindBy(a => a.Id == Id);
            dal.Remove(saleToBeDeleted);

            return Results.NoContent();
        });
    }
}
