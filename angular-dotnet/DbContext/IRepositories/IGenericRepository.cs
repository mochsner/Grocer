using System.Linq.Expressions;

namespace angular_dotnet.Repositories;

public interface IGenericRepository < T > where T: class {
    T? GetById(Guid id);
    IEnumerable < T > GetAll();
    IEnumerable < T > Find(Expression < Func < T, bool >> expression);
    // void Add(T entity);
    T? Add(T entity);
    void AddRange(IEnumerable < T > entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable < T > entities);
}