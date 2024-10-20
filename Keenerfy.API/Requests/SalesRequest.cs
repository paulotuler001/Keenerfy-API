namespace Keenerfy.API.Requests;
public record SalesRequest(
    DateTime Date, 
    int Quantity, 
    ProductsRequest productRequest
    );
