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

        groupBuilder.MapGet("/", ([FromServices] DAL<Product> dal) =>
        {
            return dal.List();
        });

        groupBuilder.MapGet("/by-name/{name}", ([FromServices] DAL<Product> dal, string name) =>
        {
            return dal.FindBy(a => a.Name.ToUpper().Equals(name.ToUpper()));
        });
        
        groupBuilder.MapGet("/by-code/{Code}", ([FromServices] DAL<Product> dal, string Code) =>
        {
            var Product = dal.FindBy(a => a.Code.ToUpper().Equals(Code.ToUpper()));
            var ProductDTO = new
            {
                Product.Code,
                Product.Link,
                Product.Description,
                Product.Price,
                Product.Name,
                Product.Stock
            };

            return ProductDTO;
        });

        groupBuilder.MapPost("/{userId}", ([FromServices] DAL<Product> dal, [FromBody] ProductsRequest productRequest, string userId) =>
        {
            var productToCreate = new Product(productRequest.Name, productRequest.Code, productRequest.Description, productRequest.Price, productRequest.Link, productRequest.Stock);
            var alreadyExisted = dal.FindBy(a => a.Code.ToUpper().Equals(productRequest.Code.ToUpper()));

            dal.Create(productToCreate);

            if(alreadyExisted is not null)
            {
                return Results.BadRequest("This code already exist in our system");
            }

            if(productRequest.Stock > 0)
            {
                DAL<PurchaseOrder> purchaseOrderDal = new DAL<PurchaseOrder>(new KeenerfyContext());

                PurchaseOrder purchaseOrder = new(DateTime.Now, productRequest.Stock, productToCreate.Id, userId);
                purchaseOrderDal.Create(purchaseOrder);
            }
            return Results.Ok(productToCreate);
        });

        groupBuilder.MapPut("/", ([FromServices] DAL<Product> dal, [FromBody] ProductsRequestEdit productRequestEdit) =>
        {
            var productToUpdate = dal.FindBy(a => a.Code.Equals(productRequestEdit.Code));
            if (productToUpdate is null)
            {
                return Results.NotFound();
            }
            productToUpdate.Price = productRequestEdit.Price;
            productToUpdate.Stock = (int)productRequestEdit.Stock;
            productToUpdate.Link = productRequestEdit.Link;
            productToUpdate.Name = productRequestEdit.Name;
            productToUpdate.Description = productRequestEdit.Description;

            dal.Update(productToUpdate);
            return Results.Ok(productToUpdate);
        });

        groupBuilder.MapDelete("/{Code}", ([FromServices] DAL<Product> dal, string Code) =>
        {
            var productToDelete = dal.FindBy(prod => prod.Code.ToUpper().Equals(Code.ToUpper()));
            dal.Remove(productToDelete);

            return Results.NoContent();
        });

        groupBuilder.MapPut("/{Code}", ([FromServices] DAL<Product> dal, string Code) =>
        {
            var productToUpdate = dal.FindBy(a => a.Code.Equals(Code));
            if (productToUpdate is null)
            {
                return Results.NotFound();
            }

            productToUpdate.Stock -= 1;

            dal.Update(productToUpdate);

            return Results.Ok();
        });
    }
}
