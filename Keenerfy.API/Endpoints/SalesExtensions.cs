using Keenerfy.API.Requests;
using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Microsoft.AspNetCore.Identity;
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
            var saleDTO = SaleList.Select(s => new
            {
                s.Quantity,
                s.Product.Code,
                s.Product.Name
            });

            return Results.Ok(saleDTO);
        });

        groupBuilder.MapGet("/{Id}", ([FromServices] DAL<Sale> dal, string Id) =>
        {

            IEnumerable<Sale> SaleList = dal.List();

            var FilteredSaleList = SaleList.Where(sale => sale.Users.Id.Equals(Id));

            var saleDTO = FilteredSaleList.Select(s => new
            {
                s.Quantity,
                s.Product.Code,
                s.Product.Name
            });

            return saleDTO;
        });

        groupBuilder.MapPost("/{Id}", ([FromServices] DAL<Sale> dal, SalesRequest salesRequest, string Id) =>
        {
            DAL<Product> dalProduct = new DAL<Product>(new KeenerfyContext());

            Product productToBeRelated = dalProduct.FindBy(prod => prod.Code.Equals(salesRequest.ProductCode));

            var sales = new Sale(DateTime.Now, salesRequest.Quantity, productToBeRelated.Id, Id);
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
