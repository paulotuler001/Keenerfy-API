using Keenerfy.Models;

namespace Keenerfy.API.Requests;
public record UsersRequest(
    string Cpf,
    string Email,
    string Password,
    string UserName
    );