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
                Console.WriteLine("Digite o nome do seu novo produto");
        string product_name = Console.ReadLine();
        Console.WriteLine("Descrição");
        string description = Console.ReadLine();
        Console.WriteLine("Code");
        string code = Console.ReadLine();
        Product product1 = new(product_name, code.ToString(), description, 105.4f, null, 1);
        product.Create(product1);
    }
    public IEnumerable<Product> GetAll(DAL<Product> product)
    {
        return product.List();
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
        product.Remove(productFound);
    }
}
