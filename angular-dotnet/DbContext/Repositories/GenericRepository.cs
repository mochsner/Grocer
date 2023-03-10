using System.Linq.Expressions;
using angular_dotnet.DbContext;

namespace angular_dotnet.Repositories;

public class GenericRepository < T > : IGenericRepository < T > where T: class {
    
    protected readonly GrocerContext context;
    
    public GenericRepository(GrocerContext context) {
        this.context = context;
    }
    // public void Add(T entity) {
    //     context.Set < T > ().Add(entity);
    // }
    public T Add(T entity) {
        var sEntityEntry = context.Set < T > ().Add(entity);
        return sEntityEntry.Entity;
    }
    public void AddRange(IEnumerable < T > entities) {
        context.Set < T > ().AddRange(entities);
    }
    public IEnumerable < T > Find(Expression < Func < T, bool >> expression) {
        return context.Set < T > ().Where(expression);
    }
    public IEnumerable < T > GetAll() {
        return context.Set < T > ().ToList();
    }

    public T? GetById(Guid id) {
        return context.Set < T > ().Find(id);
    }
    public void Remove(T entity) {
        context.Set < T > ().Remove(entity);
    }
    public void RemoveRange(IEnumerable < T > entities) {
        context.Set < T > ().RemoveRange(entities);
    }
}