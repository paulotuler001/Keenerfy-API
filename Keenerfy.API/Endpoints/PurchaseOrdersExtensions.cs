using Keenerfy.Keenerfy.Database;
using Keenerfy.Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keenerfy.API.Endpoints;
public static class PurchaseOrdersExtensions
{
    public static void PurchaseOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/purchase-order", ([FromServices] DAL<PurchaseOrder> dal) =>
        {
            return dal.List();
        });
    }
}
