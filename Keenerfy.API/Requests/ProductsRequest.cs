using Keenerfy.Models;

namespace Keenerfy.API.Requests;
public record ProductsRequest(
    string Name, 
    string Code, 
    string Description, 
    float Price,
    byte[]? Link,
    int Stock
    );