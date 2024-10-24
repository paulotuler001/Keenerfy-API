
using Keenerfy.API.Requests;
using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Keenerfy.Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keenerfy.API.Endpoints;
public static class PurchaseOrdersExtensions
{
    public static void PurchaseOrderEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("purchase-order").RequireAuthorization()
           .WithTags("PurchaseOrder");

        groupBuilder.MapGet("/", ([FromServices] DAL<PurchaseOrder> dal) =>
        {
            IEnumerable<PurchaseOrder> purchaseOrders = dal.List();

            var purchaseOrdersDTO = purchaseOrders.Select(s => new
            {
                s.Quantity,
                s.Product.Price,
                s.Product.Name
            });
            return Results.Ok(purchaseOrdersDTO);
        });
    }
}
