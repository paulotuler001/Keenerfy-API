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
        var groupBuilder = app.MapGroup("sales").RequireAuthorization()
            .WithTags("Sales");

        groupBuilder.MapGet("/", (HttpContext httpContext, [FromServices] DAL<Sale> dal) =>
        {
            IEnumerable<Sale> SaleList = dal.List();
            if(SaleList is null || !SaleList.Any())
            {
                return Results.BadRequest();
            }
            return Results.Ok();
        });

        groupBuilder.MapGet("/{Id}", ([FromServices] DAL<Sale> dal, int Id) =>
        {
            return dal.FindBy(a => a.Id.Equals(Id));
        });

        groupBuilder.MapPost("/", ([FromServices] DAL<Sale> dal, SalesRequest salesRequest) =>
        {
            DAL<Product> dalProduct = new DAL<Product>(new KeenerfyContext());

            Product productToBeRelated = dalProduct.FindBy(prod => prod.Code.Equals(salesRequest.productRequest.Code));

            var sales = new Sale(salesRequest.Date, salesRequest.Quantity, productToBeRelated);
            dal.Create(sales);

            if(salesRequest.Quantity <= productToBeRelated.Stock)
                productToBeRelated.Stock -= salesRequest.Quantity;
            else
                return Results.BadRequest("You can't buy more products than the existing ones");

            dalProduct.Update(productToBeRelated);
            return Results.Ok(sales);
        });

        groupBuilder.MapDelete("/{Id}", ([FromServices] DAL<Sale> dal, int Id) =>
        {
            Sale saleToBeDeleted = dal.FindBy(a => a.Id.Equals(Id));
            dal.Remove(saleToBeDeleted);

            return Results.NoContent();
        });
    }
}
