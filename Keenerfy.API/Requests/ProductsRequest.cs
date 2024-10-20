namespace Keenerfy.API.Requests;
public record ProductsRequest(
    string Name, 
    string Code, 
    string Description, 
    float Price, 
    string? Link, 
    int Stock_id
    );