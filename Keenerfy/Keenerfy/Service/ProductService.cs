using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keenerfy.Keenerfy.Service;
internal class ProductService
{
    public void NewProduct(DAL<Product> product)
    {
        Product product1 = new();

        Console.WriteLine("Digite o nome do seu novo produto");
        string product_name = Console.ReadLine();
        Console.WriteLine("Descrição");
        string description = Console.ReadLine();
        Console.WriteLine("Code");
        string code = Console.ReadLine();
        product1.Name = product_name;
        product1.Description = description;
        product1.Code = code.ToString();
        product1.Price = 105.4f;
        product.Adicionar(product1);
    }
    public IEnumerable<Product> GetAll(DAL<Product> product)
    {
        return product.Listar();
    }
    public void UpdateProduct(DAL<Product> product)
    {
        Console.WriteLine("Digite o nome do product que voce deseja editar");
        string productName = Console.ReadLine();
        Product productFound = product.FindBy(a => a.Name.Equals(productName));
        Console.WriteLine("Digite o novo nome");
        productFound.Name = Console.ReadLine();
        product.Update(productFound);
    }
    public void RemoveProduct(DAL<Product> product)
    {
        Console.WriteLine("Digite o nome do product que voce deseja editar");
        string productName = Console.ReadLine();
        Product productFound = product.FindBy(a => a.Name.Equals(productName));
        product.Remover(productFound);
    }
}
