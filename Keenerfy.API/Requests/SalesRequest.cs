namespace Keenerfy.API.Requests;
public record SalesRequest(
    int Quantity, 
    string ProductCode,
    string name
    );
