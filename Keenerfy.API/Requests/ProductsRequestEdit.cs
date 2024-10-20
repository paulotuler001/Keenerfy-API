namespace Keenerfy.API.Requests;
public record ProductsRequestEdit(
        int Id, 
        string Name, 
        string Code, 
        string Description, 
        float Price, 
        string Link
    );
