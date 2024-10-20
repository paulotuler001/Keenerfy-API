using Keenerfy.Database;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Keenerfy.Service;
using Keenerfy.Models;

var context = new KeenerfyContext();

ProductService prod = new();

DAL<Product> product = new(context);

//prod.NewProduct(product);
prod.GetAll(product);
//prod.UpdateProduct(product);
//prod.RemoveProduct(product);
