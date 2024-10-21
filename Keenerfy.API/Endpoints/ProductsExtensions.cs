using Keenerfy.API.Requests;
using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Keenerfy.Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keenerfy.API.Endpoints;
public static class ProductsExtensions
{
    public static void ProductsEndpoints(this WebApplication app)
    {

        var groupBuilder = app.MapGroup("products").RequireAuthorization()
            .WithTags("Products");

        groupBuilder.MapGet("/products", ([FromServices] DAL<Product> dal) =>
        {
            return dal.List();
        });

        groupBuilder.MapGet("/products/{name}", ([FromServices] DAL<Product> dal, string name) =>
        {
            return dal.FindBy(a => a.Name.ToUpper().Equals(name.ToUpper()));
        });

        groupBuilder.MapPost("/products", ([FromServices] DAL<Product> dal, [FromBody] ProductsRequest productRequest, int quantity) =>
        {
            var productToCreate = new Product(productRequest.Name, productRequest.Code, productRequest.Description, productRequest.Price, productRequest.Link, productRequest.Stock);
            dal.Create(productToCreate);
            if(quantity > 0)
            {
                DAL<PurchaseOrder> purchaseOrderDal = new DAL<PurchaseOrder>(new KeenerfyContext());

                PurchaseOrder purchaseOrder = new(DateTime.Now, quantity, productToCreate);
                purchaseOrderDal.Create(purchaseOrder);
            }

            return Results.Ok(productToCreate);
        });

        groupBuilder.MapPut("/products", ([FromServices] DAL<Product> dal, [FromBody] ProductsRequestEdit productRequestEdit) =>
        {
            var productToUpdate = dal.FindBy(a => a.Id.Equals(productRequestEdit.Id));
            if (productToUpdate is null)
            {
                return Results.NotFound();
            }
            dal.Update(productToUpdate);
            return Results.Ok(productToUpdate);
        });

        groupBuilder.MapDelete("/products/{Code}", ([FromServices] DAL<Product> dal, [FromBody] string Code) =>
        {
            var productToDelete = dal.FindBy(prod => prod.Code.ToUpper().Equals(Code.ToUpper()));
            dal.Remove(productToDelete);

            return Results.NoContent();
        });
    }
}
