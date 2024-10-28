namespace Keenerfy.API.Requests;
public record ProductsRequestEdit(
        string? Name, 
        string? Code, 
        string? Description, 
        float? Price, 
        byte[]? Link,
        int? Stock
    );
