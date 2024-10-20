using Keenerfy.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keenerfy.Keenerfy.Database;
public class DAL<T> where T : class
{
    private readonly KeenerfyContext context;

    public DAL(KeenerfyContext context)
    {
        this.context = context;
    }
    public IEnumerable<T> Listar()
    {
        return context.Set<T>().ToList();
    }
    public void Adicionar(T obj)
    {
        context.Set<T>().Add(obj);
        context.SaveChanges();
    }
    public void Remover(T obj)
    {
        context.Set<T>().Remove(obj);
        context.SaveChanges();
    }
    public void Update(T obj)
    {
        context.Set<T>().Update(obj);
        context.SaveChanges();
    }
    public T? FindBy(Func<T, bool> condition)
    {
        return context.Set<T>().FirstOrDefault(condition);
    }

}

