using Keenerfy.Database;

namespace Keenerfy.Keenerfy.Database;
public class DAL<T> where T : class
{
    private readonly KeenerfyContext context;

    public DAL(KeenerfyContext context)
    {
        this.context = context;
    }
    public IEnumerable<T> List()
    {
        return context.Set<T>().ToList();
    }
    public void Create(T obj)
    {
        context.Set<T>().Add(obj);
        context.SaveChanges();
    }
    public void Remove(T obj)
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

